using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public abstract class MovementBehaviour : MonoBehaviour
{
    [SerializeField] protected EntityMediator enemy;

    [SerializeField] protected Rigidbody2D enemyRigidbody;
    public Rigidbody2D EnemyRigidbody => enemyRigidbody;

    public float FacingDirection { get; protected set; }

    protected MovementModifier[] modifiers;

    [SerializeField] private List<EnemyStateProfile> moveStateProfiles;
    public List<EnemyStateProfile> MovementStateProfiles => moveStateProfiles;

    private bool facingBlocked;
    public bool FacingBlocked { get { return facingBlocked; } set { facingBlocked = value; } }

    private void OnDisable()
    {
        enemy.FSM.stateMachine.OnStateChange -= HandleStateChanged;
        DisableAllModifiers();
    }

    private void Awake()
    {
        modifiers = GetComponents<MovementModifier>();
        DisableAllModifiers();
    }


    private void Start()
    {
        enemy.FSM.stateMachine.OnStateChange += HandleStateChanged;

        if (transform.localScale.x < 0)
            FacingDirection = -1;
        else
            FacingDirection = 1;
    }

    private void Update()
    {
        HandleFlip();
    }

    private void HandleStateChanged(EnemyStateID stateId)
    {
        DisableAllModifiers();
        CheckModsForEnable(stateId);
    }

    public void CheckModsForEnable(EnemyStateID stateId)
    {
        var binding = moveStateProfiles.Find(x => x.stateID == stateId);

        if (binding != null && binding.movementProfile != null)
        {
            SetActiveModifiers(binding.movementProfile.Modifiers);
        }
    }

    protected void MoveEnemy(Vector2 direction)
    {
        enemyRigidbody.velocity = direction;
    }

    public void SetActiveModifiers(List<MovementModifierID> activeIDs)
    {
        foreach (var mod in modifiers)
            mod.enabled = activeIDs.Contains(mod.ID);
    }

    public void DisableAllModifiers()
    {
        foreach (var mod in modifiers)
            mod.enabled = false;
    }

    protected Vector2 ApplyTargetModifiers(Vector2 target, EntityMediator enemy)
    {
        Vector2 finalTarget = target;
        Vector2? result = null;

        foreach (var mod in modifiers)
            if (mod.enabled)
            {
                result = mod.ModifyTarget(enemy, finalTarget);

                if (result.HasValue)
                    finalTarget = result.Value;
            }
        return finalTarget;
    }

    protected float ApplySpeedModifiers(Vector2 target, EntityMediator enemy, float speed, float totalTime = 0f, float remainingTime = 0f)
    {
        float newSpeed = 1f;

        foreach (var mod in modifiers)
            if (mod.enabled)
                newSpeed *= mod.ModifySpeed(enemy, target, speed, totalTime, remainingTime);

        return newSpeed;
    }

    public abstract void MoveTo(Vector2 position, float speed, Action reached = null);
    public abstract void MoveToDirection(Vector2 direction, float speed);

    public void DecelerateToVelocity(Vector2 targetVelocity, float duration, bool useCurve = false, AnimationCurve curve = default, Action onComplete = null)
    {
        float time = 0f;

        Vector2 startVelocity = enemyRigidbody.velocity;

        TimersManager.Instance.StartRuntimeTimer(duration, () =>
        {
            onComplete?.Invoke();
        }, 0, null, (remaing) =>
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / duration);

            if (useCurve)
                 t = curve.Evaluate(t);

            Vector2 newVelocity = Vector2.Lerp(startVelocity, targetVelocity, t);

            MoveEnemy(newVelocity);
        });
    }

    public virtual void StopMovement()
    {
        enemyRigidbody.velocity = Vector2.zero;
    }

    public void HandleFlip()
    {
        float velocityX = enemyRigidbody.velocity.x;

        if (Mathf.Abs(velocityX) > 0.001f)
        {
            float dir = Mathf.Sign(velocityX);
            if (dir != FacingDirection)
                Flip();
        }
    }

    public void Flip()
    {
        if (FacingBlocked) return;
        
        FacingDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * FacingDirection;
        transform.localScale = scale;
    }

    public bool CheckIfIsOnSamePositionAsPlayer()
    {
        if (Mathf.Abs(enemy.E_Vision.playerGameObject.transform.position.x - transform.position.x) < 0.2f)
            return true;

        return false;
    }

    public bool CheckIfPlayerIsToHigh()
    {
        if (Mathf.Abs(enemy.E_Vision.playerGameObject.transform.position.y - transform.position.y) > 5f)
            return true;

        return false;
    }

    #region Knockback Functions
    public void AddKnockback(float knockbackForce, KnockbackDirectionEnum direction)
    {
        Vector2 dir = DirectionFromEnum(direction);

        ApplyKnockback(knockbackForce, dir);

        float originalDrag = enemyRigidbody.drag;
        enemyRigidbody.drag = 10f;

        TimersManager.Instance.StartRuntimeTimer(enemy.E_Stats.BaseStats.KnockbackTime, () =>
        {
            enemyRigidbody.drag = originalDrag;
            enemyRigidbody.velocity = Vector2.zero;
            enemy.E_LifeController.KockbackEnded();
        });
    }

    private void ApplyKnockback(float knockbackForce, Vector2 direction)
    {
        float resistanceMult = Mathf.Clamp01(1f - (enemy.E_Stats.BaseStats.KnockbackResistance / 100f));
        resistanceMult = Mathf.Max(resistanceMult, 0.05f);

        enemyRigidbody.velocity = Vector2.zero;

        Vector2 force = direction.normalized * knockbackForce * resistanceMult;

        enemyRigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private Vector2 DirectionFromEnum(KnockbackDirectionEnum direction)
    {
        return direction switch
        {
            KnockbackDirectionEnum.Up => Vector2.up,
            KnockbackDirectionEnum.Down => Vector2.down,
            KnockbackDirectionEnum.Left => Vector2.left,
            KnockbackDirectionEnum.Right => Vector2.right,
            _ => Vector2.zero
        };
    }

    public KnockbackDirectionEnum GetPlayerKnockbackDirection()
    {
        return enemy.E_Vision.playerGameObject.transform.position.x < transform.position.x ? KnockbackDirectionEnum.Left : KnockbackDirectionEnum.Right;
    }
    #endregion
}

[System.Serializable] 
public class EnemyStateProfile 
{ 
    public EnemyStateID stateID; 
    public MovementProfile movementProfile; 
}
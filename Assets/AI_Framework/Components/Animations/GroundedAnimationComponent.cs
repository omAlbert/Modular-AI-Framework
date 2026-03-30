using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedAnimationComponent : MonoBehaviour
{
    private EntityMediator enemy;

    [Header("Animation States")]
    [SerializeField] private AnimationID idleAnim;
    [SerializeField] private AnimationID walkAnim;
    [SerializeField] private AnimationID runAnim;

    private bool doingOverride;

    private bool isWalking;

    public bool SetOverride { get { return doingOverride; } set { doingOverride = value; } }
    public bool IsWalking { get { return isWalking; } set { isWalking = value; } }

    void Awake()
    {
        enemy = GetComponent<EntityMediator>();
    }

    void Update()
    {
        if (doingOverride) return;

        float velocityX = Mathf.Abs(enemy.E_Movement.EnemyRigidbody.velocity.x);

        if (velocityX > 0.1f)
        {
            enemy.E_Animations.ChangeAnimationState(isWalking ? walkAnim : runAnim);
        }
        else
        {
            enemy.E_Animations.ChangeAnimationState(idleAnim);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRequirementsComponent : MonoBehaviour
{
    private EntityMediator enemy;

    private float lastTargetDetectionTime;

    private void OnEnable()
    {
        enemy.E_Vision.OnPlayerDetected += HandlePlayerDetection;
    }

    private void OnDisable()
    {
        enemy.E_Vision.OnPlayerDetected -= HandlePlayerDetection;
    }

    private void Awake()
    {
        enemy = GetComponent<EntityMediator>();
    }

    public bool CanUseAttack(EnemyAttackData attack)
    {
        //if (enemy.E_Context.IsStunned) return false;
        //if (!enemy.IsAlive) return false;

        if (attack.RequiresGrounded && !enemy.E_Context.IsGrounded)
            return false;

        if (Time.time < lastTargetDetectionTime + attack.EngagementDelay)
            return false;

        if (enemy.E_AttackContext.AttacksOnCooldown.Contains(attack))
            return false;
        //if (attack.RequiresFacingPlayer && !enemy.IsFacingPlayer())
        //  return false;

        if (!enemy.E_Vision.IsPlayerInRange(attack.Range))
            return false;

        return true;
    }

    private void HandlePlayerDetection()
    {
        lastTargetDetectionTime = Time.time;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GroundedMovementComponent : MovementBehaviour
{
    public override void MoveTo(Vector2 target, float speed, Action reached = null)
    {
        Vector2 modifiedTarget = ApplyTargetMods(target);

        if (modifiedTarget == Vector2.zero)
        {
            MoveEnemy(new Vector2(0, enemyRigidbody.velocity.y));
            return;
        }

        float distance = modifiedTarget.x - transform.position.x;

        if (Mathf.Abs(distance) < 0.05f || enemy.E_Context.IsWallAhead)
        {
            MoveEnemy(new Vector2(0, enemyRigidbody.velocity.y));
            reached?.Invoke();
            return;
        }



        float speedMultiplier = ApplySpeedMods(modifiedTarget, speed);

        float direction = Mathf.Sign(modifiedTarget.x - transform.position.x);

        if (direction != FacingDirection)
            Flip();

        MoveEnemy(new Vector2(direction * speed * speedMultiplier, enemyRigidbody.velocity.y));
    }

    public override void MoveToDirection(Vector2 direction, float speed)
    {
        Vector2 modifiedTarget = ApplyTargetMods(direction);

        float speedMultiplier = ApplySpeedMods(modifiedTarget, speed);

        if (direction.x != FacingDirection)
            Flip();

        direction.Normalize();

        MoveEnemy(new Vector2(direction.x * speed * speedMultiplier, enemyRigidbody.velocity.y));
    }

    public override void StopMovement()
    {
        enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
    }

    private Vector2 ApplyTargetMods(Vector2 target)
    {
        return ApplyTargetModifiers(target, enemy);
    }

    private float ApplySpeedMods(Vector2 target, float speed)
    {
        float totatlDuration = 0;
        float remainingTime = 0;

        if (enemy.E_AttackContext.CurrentAttackContext.AttackData != null)
        {
            totatlDuration = enemy.E_AttackContext.CurrentAttackContext.TotalDuration;
            remainingTime = enemy.E_AttackContext.CurrentAttackContext.RemainingTime;
        }

        return ApplySpeedModifiers(target, enemy, speed, totatlDuration, remainingTime);
    }
}

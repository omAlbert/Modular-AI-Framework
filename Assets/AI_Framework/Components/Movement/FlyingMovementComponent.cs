using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovementComponent : MovementBehaviour
{
    public override void MoveTo(Vector2 target, float speed, Action reached = null)
    {
        Vector2 distance = target - (Vector2)transform.position;

        if (CheckIfIsOnSamePositionAsPlayer() || CheckIfPlayerIsToHigh()) return;

        if (Mathf.Abs(distance.x) < 0.1f && Mathf.Abs(distance.y) < 0.05f)
        {
            MoveEnemy(new Vector2(0, enemyRigidbody.velocity.y));
            reached?.Invoke();
            return;
        }

        Vector2? modifiedTarget = ApplyTargetModifiers(target, enemy);

        Vector2 direction = ((Vector2)modifiedTarget - (Vector2)transform.position).normalized;

        //if (direction.normalized.x != FacingDirection)
            //Flip();

        MoveEnemy(direction.normalized * speed);
    }

    public override void MoveToDirection(Vector2 direction, float speed)
    {
    }
}

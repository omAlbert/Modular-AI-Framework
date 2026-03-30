using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MaintainDistanceModifier : MovementModifier
{
    [SerializeField] private float idealDistance;
    [SerializeField] private float tolerance;

    [SerializeField] private bool maintainPlayerFacing;

    private EntityMediator enemy;

    private void OnDisable()
    {
        enemy.E_Movement.FacingBlocked = false;
    }

    public override Vector2? ModifyTarget(EntityMediator enemy, Vector2 target)
    {
        if (maintainPlayerFacing) 
            enemy.E_Movement.FacingBlocked = true;

        if (this.enemy == null)
            this.enemy = enemy;

        Vector2 enemyPos = enemy.transform.position;

        float deltaX = target.x - enemyPos.x;
        float absDistance = Mathf.Abs(deltaX);

        if (Mathf.Abs(absDistance - idealDistance) <= tolerance)
            return Vector2.zero;

        float dir = Mathf.Sign(deltaX);

        Vector2 newTarget = new Vector2(
            target.x - dir * idealDistance,
            target.y
        );

        return newTarget;
    }
} 
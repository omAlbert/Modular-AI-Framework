using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackBehaviourComponent : AttacksBehaviour
{
    public override void Execute()
    {
        DashAttackData dashData = (DashAttackData)enemy.E_AttackContext.CurrentAttackContext.AttackData;

        enemy.E_AttackContext.CurrentAttackContext.TotalDuration = dashData.DashDuration;

        Vector2 target = Vector2.zero;

        if (dashData.MoveTowardsPlayer)
            target = enemy.E_Vision.playerGameObject.transform.position;

        Vector2 dir = (enemy.E_Vision.playerGameObject.transform.position - transform.position);

        TimersManager.Instance.StartRuntimeTimer(dashData.DashDuration, () =>
        {
            if (dashData.DecelerateAfterAttack)
            {
                enemy.E_Movement.DecelerateToVelocity(dashData.MinVelocityDeceleration, dashData.DecelerateDuration, dashData.UseCurveToDecelerate,
                    dashData.DecelerationCurve, () =>
                {
                    enemy.E_AttackContext.AttackEnded(dashData);
                });
            }
            else
                enemy.E_AttackContext.AttackEnded(dashData);

        }, 0, null, (remaining) =>
        {
            enemy.E_AttackContext.CurrentAttackContext.RemainingTime = remaining;
            enemy.E_Movement.MoveToDirection(dir, dashData.DashSpeed);
        });
    }

}

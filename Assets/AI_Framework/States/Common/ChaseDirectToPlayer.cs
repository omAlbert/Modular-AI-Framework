using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Logic/Common/Chase Direct To Player")]
public class ChaseDirectToPlayer : EnemyStateSOBase
{
    [SerializeField] private EnemyStateID onPlayerInAttackRangeStateName;
    [SerializeField] private EnemyStateID onPlayerLostStateName;
    [SerializeField] private float chaseSpeed;

    public override void DoFrameUpdateLogic()
    {
        if (enemy.E_AttacksBehaviour && enemy.E_AttackSelector.HasAvailableAttack(enemy.E_AttacksLoadout.Attacks))
        {
            fsm.stateMachine.ChangeState(fsm.GetStateByName(onPlayerInAttackRangeStateName));
        }

        if (!enemy.E_Context.IsPlayerVisible && (!enemy.E_Context.IsPlayerInAlertRange || enemy.E_Context.IsWallAhead))
        {
            fsm.stateMachine.ChangeState(fsm.GetStateByName(onPlayerLostStateName));
        }
    }

    public override void DoPhysicsLogic()
    {
        enemy.E_Movement.MoveTo(enemy.E_Vision.playerGameObject.transform.position, chaseSpeed);
    }
}

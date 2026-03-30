using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wander", menuName = "Enemy Logic/Common/Wander")]
public class WanderState : EnemyStateSOBase
{
    [SerializeField] private EnemyStateID onPlayerSeenStateName;
    [SerializeField] private float speed;

    public override void DoFrameUpdateLogic()
    {
        if (enemy.E_Context.IsPlayerVisible)
        {
            fsm.stateMachine.ChangeState(fsm.GetStateByName(onPlayerSeenStateName));
        }
    }

    public override void DoPhysicsLogic()
    {
        enemy.E_Territory.WanderAroundSpawnPoint(speed);
    }
}

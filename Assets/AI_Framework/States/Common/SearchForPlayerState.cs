using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(menuName = "Enemy Logic/Common/Search for Player")]
public class SearchForPlayerState : EnemyStateSOBase
{
    [SerializeField] private float timeSearching;
    [SerializeField] private float intervalToFlip;

    [SerializeField] private EnemyStateID onPlayerLostStateName;
    [SerializeField] private EnemyStateID onPlayerSeenStateName;

    [SerializeField] private float speed;

    private bool lastPlayerPositionReached;
   
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (enemy.E_Context.IsPlayerVisible)
        {
            fsm.stateMachine.ChangeState(fsm.GetStateByName(onPlayerSeenStateName));
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        if (!lastPlayerPositionReached)
        {
            enemy.E_Movement.MoveTo(enemy.E_Vision.LastKnownPlayerPosition, speed, () =>
            {
                lastPlayerPositionReached = true;

                TimersManager.Instance.StartRuntimeTimer(timeSearching, () =>
                {
                    fsm.stateMachine.ChangeState(fsm.GetStateByName(onPlayerLostStateName));
                }, intervalToFlip, () =>
                {
                    enemy.E_Movement.Flip();
                });
            });
        }
    }
}

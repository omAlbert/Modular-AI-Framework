using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Idle at point", menuName = "Enemy Logic/Common/Idle at point")]
public class IdleAtPointState : EnemyStateSOBase
{
    [SerializeField] private AnimationID animationName;
    [SerializeField] private EnemyStateID onPlayerSeeingState;

    public override void DoEnterLogic()
    {
        if (animationName)
        {
            enemy.E_Animations.ChangeAnimationState(animationName);
            enemy.E_Animations.DoOverride = true;
        }
    }

    public override void DoExitLogic()
    {
        enemy.E_Animations.DoOverride = false;
    }

    public override void DoFrameUpdateLogic()
    {
         if (enemy.E_Context.IsPlayerVisible)
         {
             fsm.stateMachine.ChangeState(fsm.GetStateByName(onPlayerSeeingState));
         }
    }
}

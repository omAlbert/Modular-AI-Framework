using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetected", menuName = "Enemy Logic/Common/Player Detected")]

public class PlayerDetected : EnemyStateSOBase
{
    [SerializeField] private AnimationID animationName;
    [SerializeField] private EnemyStateID afterPlayerDetectedNextState;

    public override void DoAnimationTriggerEventLogic(FSM_Controller.AnimationTriggerType triggerType)
    {
        if (triggerType == FSM_Controller.AnimationTriggerType.PlayerDetected)
        {
            fsm.stateMachine.ChangeState(fsm.GetStateByName(afterPlayerDetectedNextState));
        }
    }

    public override void DoEnterLogic()
    {
        enemy.E_Animations.ChangeAnimationState(animationName);
        enemy.E_Animations.DoOverride = true;
    }

    public override void DoExitLogic()
    {
        enemy.E_Animations.DoOverride = false;
    }
}

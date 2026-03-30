using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConcreteState : EnemyState
{
    private EnemyStateSOBase stateInstance;
    public EnemyConcreteState(FSM_Controller fsm, EnemyStateMachine enemyStateMachine, EnemyStateID stateID) : base (fsm, enemyStateMachine, stateID)
        //: base (fsm, enemyStateMachine, stateID)
    {
        stateInstance = GetStateInstance();
    }
    public override void AnimationTriggerEvent(FSM_Controller.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        stateInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EneterState()
    {
        base.EneterState();
        stateInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        stateInstance.DoExitLogic();

    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        stateInstance.DoFrameUpdateLogic();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        stateInstance.DoPhysicsLogic();

    }

    private EnemyStateSOBase GetStateInstance()
    {
        foreach (var state in fsm.enemyStates)
        {
            if (state.stateID == this.stateID)
            {
                return state.stateInstance;
            }
        }

        return null;
    }
}

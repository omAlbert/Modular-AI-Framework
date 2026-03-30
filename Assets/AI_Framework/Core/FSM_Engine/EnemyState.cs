using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected FSM_Controller fsm;
    protected EnemyStateMachine enemyStateMachine;
    protected EnemyStateID stateID;

    public EnemyStateID StateID => stateID;
    public EnemyState (FSM_Controller fsm, EnemyStateMachine enemyStateMachine, EnemyStateID stateID)
    {
        this.fsm = fsm;
        this.enemyStateMachine = enemyStateMachine;
        this.stateID = stateID;
    }

    public virtual void EneterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(FSM_Controller.AnimationTriggerType triggerType) { }
}

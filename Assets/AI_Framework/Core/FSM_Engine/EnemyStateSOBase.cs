using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSOBase : ScriptableObject
{
    protected FSM_Controller fsm;
    protected Transform transform;
    protected GameObject gameObject;
    protected EntityMediator enemy;

    public EnemyStateID stateID;

    public virtual void Initialize(GameObject gameObject, FSM_Controller fsm, EntityMediator enemy)
    {
        this.fsm = fsm;
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }    
    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(FSM_Controller.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}

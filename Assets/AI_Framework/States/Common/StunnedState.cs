using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stunned", menuName = "Enemy Logic/Common/Stunned")]
public class StunnedState : EnemyStateSOBase
{
    [SerializeField] private AnimationID animationName;
    [SerializeField] private EnemyStateID onStunEndStateName;

    [SerializeField] private float timeStunned;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        enemy.E_Animations.ChangeAnimationState(animationName);

        TimersManager.Instance.StartRuntimeTimer(timeStunned, () =>
        {
            fsm.stateMachine.ChangeState(fsm.GetStateByName(onStunEndStateName));
        });
    }
}

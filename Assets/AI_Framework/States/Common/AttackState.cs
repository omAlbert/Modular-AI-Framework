using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Logic/Common/AttackState")]
public class AttackState : EnemyStateSOBase
{
    [SerializeField] private EnemyStateID onAttackEndedNextState;
    public override void DoEnterLogic()
    {
        enemy.E_AttackContext.OnFinished += AttackFinished;
        enemy.E_AttacksBehaviour.Execute();
    }

    public override void DoExitLogic()
    {
        enemy.E_AttackContext.OnFinished -= AttackFinished;
    }

    private void AttackFinished()
    {
        enemy.FSM.stateMachine.ChangeState(enemy.FSM.GetStateByName(onAttackEndedNextState));
    }
}
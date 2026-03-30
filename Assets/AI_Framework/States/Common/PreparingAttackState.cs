using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Logic/Common/Prepare Attack")]
public class PreparingAttackState : EnemyStateSOBase
{
    [SerializeField] private AnimationID animationName;
    [SerializeField] private EnemyStateID onAttackReadyNextState;

    public override void DoEnterLogic()
    {
        enemy.E_Animations.ChangeAnimationState(animationName);
        enemy.E_Animations.DoOverride = true;

        enemy.E_AttackContext.OnAttackPrepared += HandleAttackReady;

        var selectedAttack = enemy.E_AttackSelector.SelectAttack(enemy.E_AttacksLoadout.Attacks);

        enemy.E_AttackContext.SetCurrentAttack(selectedAttack);
        enemy.E_Movement.StopMovement();
        enemy.E_AttackContext.StartPrepareAttack(selectedAttack);
    }

    public override void DoExitLogic()
    {
        enemy.E_Animations.DoOverride = false;
        enemy.E_AttackContext.OnAttackPrepared -= HandleAttackReady;
    }

    private void HandleAttackReady()
    {
        fsm.stateMachine.ChangeState(fsm.GetStateByName(onAttackReadyNextState));
    }
}

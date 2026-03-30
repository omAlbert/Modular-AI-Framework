using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReturnToSpawnZone", menuName = "Enemy Logic/Common/Return to Spawn Zone")]
public class ReturnToSpawnZoneState : EnemyStateSOBase
{
    [SerializeField] private float returnSpeed;
    [SerializeField] private EnemyStateID onSpawnZoneReachedStateName;

    private Vector2 positionToGo;
   
    public override void DoEnterLogic()
    {
        positionToGo = enemy.E_Territory.GetRandomPointNearSpawn();
    }

    public override void DoPhysicsLogic()
    {
        enemy.E_Movement.MoveTo(positionToGo, returnSpeed, () => {
            fsm.stateMachine.ChangeState(fsm.GetStateByName(onSpawnZoneReachedStateName));
        });
    }
}

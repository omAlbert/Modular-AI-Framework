using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentEnemyState { get; set; }

    public event Action <EnemyStateID> OnStateChange;
    public void Initialize(EnemyState startingState)
    {
        currentEnemyState = startingState;
        currentEnemyState.EneterState();
    }

    public void ChangeState(EnemyState newState)
    {
        currentEnemyState.ExitState();
        currentEnemyState = newState;
        currentEnemyState.EneterState();

        OnStateChange?.Invoke(newState.StateID);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Controller : MonoBehaviour
{
    public EnemyStateMachine stateMachine { get; set; }

    private EnemyStates[] initialStates;

    [SerializeField] public EnemyStates[] enemyStates;

    [SerializeField] private Enemy enemy;

    protected void Awake()
    {
        #region Initialize State Machine

        stateMachine = new EnemyStateMachine();
        int initialCount = 0;
        foreach (var state in enemyStates)
        {
            if (state.initialState)
            {
                initialCount++;
            }
        }

        initialStates = new EnemyStates[initialCount];

        int aux = 0;
        for (int i = 0; i < enemyStates.Length; i++)
        {
            enemyStates[i].stateInstance = Instantiate(enemyStates[i].enemyState);
            enemyStates[i].concreteState = new EnemyConcreteState(this, stateMachine, enemyStates[i].stateID);

            if (enemyStates[i].initialState)
            {
                initialStates[aux] = enemyStates[i];
                aux++;
            }
        }
        #endregion

        foreach (var state in enemyStates)
        {
            state.stateInstance.Initialize(gameObject, this, enemy);
        }

        if (initialStates.Length > 1)
        {
            stateMachine.Initialize(GetRandomInitialState());
        }
        else
        {
            stateMachine.Initialize(initialStates[0].concreteState);
        }
    }

    protected void Start()
    {
        #region Start State Machine

       

        #endregion
    }

    protected void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }

    protected void FixedUpdate()
    {
        stateMachine.currentEnemyState.PhysicsUpdate();
    }

    #region StateMachine Functions

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        stateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        Idle,
        EnenmyAttack,
        PlayerDetected,
        AttackEnded,
        EnterToScene
    }

    public EnemyConcreteState GetStateByName(EnemyStateID name)
    {
        foreach (var state in enemyStates)
        {
            if (state.stateID == name)
            {
                return state.concreteState;
            }
        }
        return null;
    }

    private EnemyConcreteState GetRandomInitialState()
    {
        return initialStates[UnityEngine.Random.Range(0, initialStates.Length)].concreteState;
    }

    public string GetCurrentEnemyStateName { get { return stateMachine.currentEnemyState.StateID.name; } }

    #endregion
}

[System.Serializable]
public struct EnemyStates
{
    public EnemyStateID stateID;

    public EnemyStateSOBase enemyState;
    public bool initialState;

    [HideInInspector] public EnemyStateSOBase stateInstance;

    [HideInInspector] public EnemyConcreteState concreteState;
}
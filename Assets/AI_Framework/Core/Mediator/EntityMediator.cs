using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityMediator : MonoBehaviour
{
    [SerializeField] private LifeControllerComponent enemyLifeController;
    public LifeControllerComponent E_LifeController => enemyLifeController;

    [SerializeField] private DamageOnTouchComponent damagePlayer;
    public DamageOnTouchComponent DamageOnTouch => damagePlayer;

    [SerializeField] private FSM_Controller fsm;
    public FSM_Controller FSM => fsm;

    [SerializeField] private AnimationComponent animations;
    public AnimationComponent E_Animations => animations;

    [SerializeField] private MovementBehaviour movement;
    public MovementBehaviour E_Movement => movement;

    [SerializeField] private EnemyStatsComponent stats;
    public EnemyStatsComponent E_Stats => stats;

    [SerializeField] private VisionComponent vision;
    public VisionComponent E_Vision => vision;

    [SerializeField] private EnvironmentCheckerComponent environment;
    public EnvironmentCheckerComponent E_Environment => environment;

    [SerializeField] private EnemyTerritoryComponent territory;
    public EnemyTerritoryComponent E_Territory => territory;

    public EnemyContext E_Context {  get; private set; }


    [SerializeField] private AttacksBehaviour attacks;
    public AttacksBehaviour E_AttacksBehaviour => attacks;

    [SerializeField] private AttackRequirementsComponent attacksRequirments;
    public AttackRequirementsComponent E_AttacksattacksRequirments => attacksRequirments;

    [SerializeField] private AttackContextComponent attackContext;
    public AttackContextComponent E_AttackContext => attackContext;

    [SerializeField] private AttacksSelectorComponent attackSelector;
    public AttacksSelectorComponent E_AttackSelector => attackSelector;
    
    [SerializeField] private EnemyAttackLoadout attacksLoadout;
    public EnemyAttackLoadout E_AttacksLoadout => attacksLoadout;

    private void Awake()
    {
        E_Context = new();
    }
}

[System.Serializable]
public class EnemyContext
{
    public bool IsPlayerVisible;
    public bool IsPlayerInAlertRange;

    public bool IsGrounded;
    public bool IsWallAhead;
    public bool IsLedgeAhead;
    public bool IsDamaged;
}
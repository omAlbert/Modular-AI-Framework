using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/Attacks/Dash Attack ID")]
public class DashAttackData : EnemyAttackData, IPrepareAttack
{
    [Space(10f)]
    [Header("Dash attack data")]
    [SerializeField] private float prepareTime;
    public float PrepareTime => prepareTime;

    [SerializeField] private float dashSpeed;
    public float DashSpeed => dashSpeed;

    [SerializeField] private float dashDuration;
    public float DashDuration => dashDuration;

    [SerializeField] private bool lockDirectionOnPrepare;
    public bool LockDirectionOnPrepare => lockDirectionOnPrepare;

    [SerializeField] private bool moveTowardsPlayer;
    public bool MoveTowardsPlayer => moveTowardsPlayer;


    [Header("Deceleration")]
    [Space(5f)]
    [SerializeField] private float decelerateDuration;
    public float DecelerateDuration => decelerateDuration;

    [SerializeField] private Vector2 minVelocityDeceleration;
    public Vector2 MinVelocityDeceleration => minVelocityDeceleration;

    [SerializeField] private bool decelerateAfterAttack;
    public bool DecelerateAfterAttack => decelerateAfterAttack;
    
    [SerializeField] private bool useCurve;
    public bool UseCurveToDecelerate => useCurve;

    [SerializeField] private AnimationCurve decelerationCurve;
    public AnimationCurve DecelerationCurve => decelerationCurve;


}


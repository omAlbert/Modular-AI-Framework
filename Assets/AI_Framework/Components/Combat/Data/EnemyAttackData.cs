using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/Attacks/Attack ID")]
public class EnemyAttackData : ScriptableObject
{
    [Header("General data")]
    [Space(5f)]
    [SerializeField] private int priority;
    public int Priority => priority;

    [SerializeField] private float baseDamage;
    public float Damage => baseDamage;

    [SerializeField] private float range;
    public float Range => range;

    [SerializeField] private float cooldown;
    public float Cooldown => cooldown;

    [SerializeField] private float recovery;
    public float Recovery => recovery;

    [SerializeField] private bool requiresGrounded;
    public bool RequiresGrounded => requiresGrounded;

    [SerializeField] private bool requiresFacingPlayer;
    public bool RequiresFacingPlayer => requiresFacingPlayer;

    [SerializeField] private float engagementDelay;
    public float EngagementDelay => engagementDelay;

    [SerializeField] private float recoveryTime;
    public float RecoveryTime => recoveryTime;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/Attacks/Explosion Attack")]
public class ExplosionAttackData : EnemyAttackData, IPrepareAttack
{
    [SerializeField] private float prepareTime;
    public float PrepareTime => prepareTime;

    [SerializeField] private float radius;
    public float Radius => radius;

    [SerializeField] private LayerMask whatCanDamage;
    public LayerMask WhatCanDamage => whatCanDamage;
}

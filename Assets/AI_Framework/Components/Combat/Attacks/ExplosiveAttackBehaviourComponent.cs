using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAttackBehaviourComponent : AttacksBehaviour
{
    public override void Execute()
    {
        ExplosionAttackData explosionData = (ExplosionAttackData)enemy.E_AttackContext.CurrentAttackContext.AttackData;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionData.Radius, explosionData.WhatCanDamage);

        foreach (Collider2D hit in hits)
        {
            // Aplicar daño
            Damageable damageable = hit.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(explosionData.Damage, TypesOfDamageEnum.Enemy, KnockbackDirectionEnum.Undefined);
            }
        }
    }
}

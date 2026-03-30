using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouchComponent : MonoBehaviour
{
    private bool blockDammageOnTouch;

    private bool playerIsTouching;

    private Damageable playerDamageable;

    [SerializeField] private int damageOnTouch;

    [SerializeField] private EntityMediator enemy;

    
    private void Update()
    {
        if (playerIsTouching)
        {
            playerDamageable.TakeDamage(damageOnTouch, TypesOfDamageEnum.Enemy, enemy.E_Movement.GetPlayerKnockbackDirection());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !blockDammageOnTouch)
        {
            if (!playerDamageable)
                playerDamageable = collision.GetComponent<Damageable>();
           
            playerIsTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsTouching = false;
        }
    }
}

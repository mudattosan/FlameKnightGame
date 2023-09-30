using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage;
    public int manaRecieve;
    public Vector2 knockBack = Vector2.zero;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossDamageable bossDamageable = collision.GetComponent<BossDamageable>();

        if(bossDamageable != null)
        {
            bool gotHitBoss = bossDamageable.Hit(attackDamage);
            
            if (gotHitBoss)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
            }
        }

         if (collision.CompareTag("Enemy"))
        {
            Damageable gameObjectDamageable = gameObject.GetComponentInParent<Damageable>();
            if (bossDamageable != null)
            {
                gameObjectDamageable.GainMana(manaRecieve);
            }
        }
        if (collision.CompareTag("Player"))
        {
            Damageable gameObjectDamageable = collision.GetComponentInParent<Damageable>();
            bool gotHit = gameObjectDamageable.Hit(attackDamage, knockBack);

            if (gotHit)
            {
                Debug.Log(collision.name + "hit for " + attackDamage);
            }
        }
    }
}

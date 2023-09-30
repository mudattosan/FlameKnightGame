using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    public int damage;
    public float timeAppear;
    public Vector2 knockBack = Vector2.zero;

    [SerializeField] private Vector2 boxDimention;
    [SerializeField] private Transform boxPosion;

    private void Start()
    {
        Destroy(gameObject, timeAppear);
    }
    public void Cast()
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(boxPosion.position, boxDimention, 0f);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Player"))
            {
                
                Damageable gameObjectDamageable = collision.GetComponentInParent<Damageable>();
                bool gotHit = gameObjectDamageable.Hit(damage, knockBack);

                if (gotHit)
                {
                    Debug.Log(collision.name + "hit for " + damage);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxPosion.position, boxDimention);
    }
}

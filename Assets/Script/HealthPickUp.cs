using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestore = 20;

    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool isHeal = damageable.Heal(healthRestore);

            if(isHeal)
                Destroy(gameObject);
        }
    }
}

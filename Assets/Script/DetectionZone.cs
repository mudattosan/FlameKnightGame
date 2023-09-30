using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;

    public List<Collider2D> detectionColliders = new List<Collider2D>();

    private Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectionColliders.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectionColliders.Remove(collision);

        if(detectionColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}

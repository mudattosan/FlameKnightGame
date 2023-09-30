using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 minValue, maxValue;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        Vector3 bounPosition = new Vector3(
            Mathf.Clamp(desiredPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(desiredPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(desiredPosition.z, minValue.z, maxValue.z));
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, bounPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}

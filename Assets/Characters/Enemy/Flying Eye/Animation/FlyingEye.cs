using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2f;
    public float wayPointReachedDistance;

    public DetectionZone biteDetectionZone;
    public List<Transform> wayPoints;

    private Animator anim;
    private Rigidbody2D rb;
    private Damageable damageable;
    private Transform nextWayPoint;

    private int wayPointNum = 0;

    public bool _hasTarget = false;

    public bool hasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool canMove
    {
        get
        {
            return anim.GetBool(AnimationStrings.canMove);
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        nextWayPoint = wayPoints[wayPointNum];
    }
    private void Update()
    {
        hasTarget = biteDetectionZone.detectionColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (damageable.isAlive)
        {
            if (canMove)
            {
                Flight();
            } else
            {
                rb.velocity = Vector3.zero;
            }
        } else
        {
            rb.gravityScale = 2f;
        }
    }
    private void Flight()
    {
        // Fly to the next waypoint
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;

        // Check if we have reached the waypoint already
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        rb.velocity = directionToWayPoint * flightSpeed;
        UpdateDirection();

        // See if we need to switch waypoints
        if(distance <= wayPointReachedDistance)
        {
            //Switch to next waypoint
            wayPointNum++;

            if(wayPointNum >= wayPoints.Count)
            {
                // Loop back to original waypoint
                wayPointNum = 0;
            }

            nextWayPoint = wayPoints[wayPointNum];
        }
    }
    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;

        if(transform.localScale.x > 0)
        {
            // Facing to the right
            if(rb.velocity.x < 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
        else
        {
            // Facing to the left
            if(rb.velocity.x > 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
    }

}

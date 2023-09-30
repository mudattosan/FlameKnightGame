using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;

    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    private Animator anim;
    private WalkableDirection _walkDirecion;
    private Damageable damageable;
    private Vector2 walkDirectionVector = Vector2.right;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public enum WalkableDirection { Right, Left };

    public bool _hasTarget;

    public WalkableDirection walkDirection { 
        get { return _walkDirecion; }
        set {
            if (_walkDirecion != value) 
            {
                // Direction Flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirecion = value; }
    }
    public bool hasTarget { 
        get { return _hasTarget; }
        private set 
        {
            _hasTarget = value;
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool canMove { get { return anim.GetBool(AnimationStrings.canMove); } }
    public float attackCooldown { 
        get 
        {
            return anim.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            anim.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value,0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    private void Update()
    {
        hasTarget = attackZone.detectionColliders.Count > 0;

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        } 
    }
    private void FixedUpdate()
    {
        if(touchingDirections.isGrounded && touchingDirections.isOnWall)
        {
            FlipDirection();
        }

        if (!damageable.lockVelocity)
        {
            if (canMove)
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }
    private void FlipDirection()
    {
        if(walkDirection == WalkableDirection.Right)
        {
            walkDirection = WalkableDirection.Left;
        } else if(walkDirection == WalkableDirection.Left)
        {
            walkDirection = WalkableDirection.Right;
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnCliffDetected()
    {
        if (touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }
}

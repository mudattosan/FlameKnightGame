using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilDistance = 0.05f;

    private CapsuleCollider2D touchingCol;
    private Animator anim;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5]; 
    private RaycastHit2D[] wallHits = new RaycastHit2D[5]; 
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnCeiling;

    public bool isGrounded { get 
        { 
            return _isGrounded;
        } private set 
        {
            _isGrounded = value;
            anim.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    public bool isOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            anim.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    public bool isOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            anim.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
       
    }
    private void Update()
    {
        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        isOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        isOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilDistance) > 0;
    }
}

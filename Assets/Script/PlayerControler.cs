using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    Vector2 moveInput;

    [SerializeField] private bool _isRunning = false;
    [SerializeField] private bool _isFacingRight = true;
    [SerializeField] private bool _isGodMod;
    [SerializeField] private bool _isBack2Human = false;

    public bool isFacingRight { get 
        { 
            return _isFacingRight;
        } private set 
        {
            if(_isFacingRight != value)
            {
                // Flip the local scale to make the player face to the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }
    public bool isRunning { get
        {
            return _isRunning;
        } private set
        {
            _isRunning = value;
            anim.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public float currentSpeed { get 
        {
            if (canMove)
            {
                if (isRunning && !touchingDirection.isOnWall)
                {
                    return runSpeed;
                }
                else
                {
                    // Idle speed is 0
                    return 0;
                }
            } else
            {
                // Movement lock
                return 0;
            }
        } }
    public bool canMove { get 
        {
            return anim.GetBool(AnimationStrings.canMove); 
        } }
    public bool lockVelocity
    {
        get
        {
            return anim.GetBool(AnimationStrings.lockVelocity);
        } set
        {
            anim.SetBool(AnimationStrings.lockVelocity, value);
        }
    }
    public bool isGodMod
    {
        get { return _isGodMod; }
        set 
        { 
            _isGodMod = value;
            anim.SetBool(AnimationStrings.isGodMod, value);
        }
    }
    public bool isBack2Human
    {
        get { return _isBack2Human; }
        set 
        { 
            _isBack2Human = value;
            anim.SetBool(AnimationStrings.isBack2Human,value);
        }
    }

    public float runSpeed = 5f;
    public float JumpForce = 10f;
    public float godModTimer = 30f;
    public float attack_1CoolDown = 5f;
    public float attack_2CoolDown = 2f;
    public float attack_3CoolDown = 3f;
    public float spAttackCoolDown = 5f;

    private float lastAttackTime;
    private bool isOnCooldown1 = false;
    private bool isOnCooldown2 = false;
    private bool isOnCooldown3 = false;
    private bool isOnCooldown4 = false;

    public GameObject manaTextPrefab;

    private Rigidbody2D rb;
    private Animator anim;
    private TouchingDirections touchingDirection;
    private Damageable damageable;
    private Attack E_attack;
    private UIManager uIManager;
    private SpellCooldown spellCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        E_attack = GetComponentInChildren<Attack>();
        uIManager = FindObjectOfType<UIManager>();
        spellCooldown = GetComponent<SpellCooldown>();
        _isGodMod = false;
        
    }
    private void Update()
    {
        if(isGodMod)
            godModTimer -= Time.deltaTime;
        Back2Human();
    }
    private void FixedUpdate()
    {
        if(!damageable.lockVelocity)
             rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);

        anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        Back2Human();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        isRunning = moveInput != Vector2.zero; // isRunning = true/false

        SetFacingDirection(moveInput);
    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !isFacingRight)
        {
            // Face to the right
            isFacingRight = true;
        }
        else if(moveInput.x < 0 && isFacingRight)
        {
            // Face to the left
            isFacingRight = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirection.isGrounded && canMove && !isGodMod)
        {
            anim.SetTrigger(AnimationStrings.isJumping);
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        } else if(context.started && touchingDirection.isGrounded && canMove && isGodMod)
        {
            anim.SetTrigger(AnimationStrings.isE_Jump);
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
    }
    public void OnAttack_1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isOnCooldown1)
            {
                if (!isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.Attack_1);
                }
                else if (isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.E_Attack_1);
                }

                lastAttackTime = Time.time;
                isOnCooldown1 = true;
                StartCoroutine(ResetCooldownAttack_1());
            }
        }
    }
    private IEnumerator ResetCooldownAttack_1()
    {
        yield return new WaitForSeconds(attack_1CoolDown);
        isOnCooldown1 = false;
    }
    public void OnAttack_2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isOnCooldown2)
            {
                if (!isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.Attack_2);
                }
                else if (isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.E_Attack_2);
                } 

                lastAttackTime = Time.time;
                isOnCooldown2 = true;
                StartCoroutine(ResetCooldownAttack_2());
            }
        }
    }
    private IEnumerator ResetCooldownAttack_2()
    {
        yield return new WaitForSeconds(attack_2CoolDown);
        isOnCooldown2 = false;
    }
    public void OnAttack_3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isOnCooldown3)
            {
                if (!isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.Attack_3);
                }
                else if (isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.E_Attack_3);
                }

                lastAttackTime = Time.time;
                isOnCooldown3 = true;
                StartCoroutine(ResetCooldownAttack_3());
            }
        }
    }
    private IEnumerator ResetCooldownAttack_3()
    {
        yield return new WaitForSeconds(attack_3CoolDown);
        isOnCooldown3 = false;
    }
    public void OnSpAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isOnCooldown4)
            {
                if (!isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.SpAttack);
                }
                else if (isGodMod)
                {
                    anim.SetTrigger(AnimationStrings.E_SpAttack);
                }

                lastAttackTime = Time.time;
                isOnCooldown4 = true;
                StartCoroutine(ResetCooldownSpAttack());
            }
        }
    }
    private IEnumerator ResetCooldownSpAttack()
    {
        yield return new WaitForSeconds(spAttackCoolDown);
        isOnCooldown4 = false;
    }
    public void OnTransform(InputAction.CallbackContext context)
    {
        if (context.started && damageable.isTransform)
        {
            anim.SetTrigger(AnimationStrings.Transform);
            isGodMod = true;
            GodMod();
            spellCooldown.UseSpell_5();
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        lockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    private void GodMod()
    {
        damageable.maxHealth = 150;
        damageable.health += 50;
        damageable.mana = 0;
        //E_attack.manaRecieve = 0;
        uIManager.manaTextPrefab = null;
        isBack2Human = false;
        runSpeed = 10f;
    }
    private void Back2Human()
    {
        if(godModTimer <= 0)
        {
            isBack2Human = true;
            isGodMod = false;
            damageable.isTransform = false;
            godModTimer = 30f;
            // E_attack.manaRecieve = 50;
            uIManager.manaTextPrefab = manaTextPrefab;
            damageable.mana = 0;
            runSpeed = 8f;
        }
    }
}

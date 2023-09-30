using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    public UnityEvent<int, int> manaChanged;

    private Animator anim;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxMana = 100;
    [SerializeField] private int _mana = 0;
    [SerializeField] private bool _isTransform = false;
    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool isInvincible = false;

    private float timeSinceHit = 0;

    public float invincibilityTimer = 0.25f;

    public GameManagerLevel1 gameManagerLevel1;

    public int maxHealth 
    { 
        get 
        { 
            return _maxHealth;
        } set
        { 
            _maxHealth = value; 
        } }
    public int health 
    {
        get 
        { 
            return _health; 
        } set
        {
            _health = value;
            healthChanged?.Invoke(_health, maxHealth);

            // if health drop down below 0, character is not alive
            if(_health <= 0)
            {
                isAlive = false;
                StartCoroutine(YouDied());
                StartCoroutine(YouDiedLevel1());
            }
        }
    }
    public int maxMana
    {
        get 
        {
            return _maxMana;  
        } set
        {
            _maxMana = value;
        }
    }
    public int mana
    {
        get
        {
            return _mana;
        } set
        {
            _mana = value;
            manaChanged?.Invoke(_mana, maxMana);

            // if mana = maxMana, player transform
            if(_mana == _maxMana)
            {
                isTransform = true;
            }
        }
    }
    public bool isTransform
    {
        get
        {
            return _isTransform;
        } set
        {
            _isTransform = value;
            anim.SetBool(AnimationStrings.isTransform, value);
        }
    }
    public bool lockVelocity { 
        get 
        {
            return anim.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            anim.SetBool(AnimationStrings.lockVelocity, value);
        }
    }
    public bool isAlive { 
        get 
        { 
            return _isAlive;
        } set 
        { 
            _isAlive = value;
            anim.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive set" + value);
        } }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit > invincibilityTimer)
            {
                // Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

        
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (isAlive && !isInvincible)
        {
            health -= damage;
            isInvincible = true;

            anim.SetTrigger(AnimationStrings.hitTrigger);
            lockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }

        // Unable to hit
        return false;
    }
    public bool Heal(int healthRestore)
    {
        if (isAlive && health < maxHealth)
        {
            int maxHeal = Mathf.Max(maxHealth - health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        } else
        {
            return false;
        }
    }
    public void GainMana(int manaRecieve)
    {
        if(isAlive && mana < maxMana)
        {

            int maxManaGained = Mathf.Max(maxMana - mana, 0);
            int actualManaGained = Mathf.Min(maxManaGained, manaRecieve);
            mana += actualManaGained;
            CharacterEvents.characterMana.Invoke(gameObject, actualManaGained);
        }
    }
    private IEnumerator YouDied()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.GameOver();
    }
    private IEnumerator YouDiedLevel1()
    {
        yield return new WaitForSeconds(2f);
        gameManagerLevel1.GamOver();
    }
    public void OnDeath()
    {
        if(health <= 0)
        {
            isAlive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDamageable : MonoBehaviour
{
    public UnityEvent<int> damageableHit;
    public UnityEvent<int, int> healthChanged;

    private Animator anim;

    [SerializeField] private int _health = 300;
    [SerializeField] private int _maxHealth = 300;
    [SerializeField] private bool _isAlive = true;

    public int health { get { return _health; } set 
        { 
            _health = value;
            healthChanged?.Invoke(_health, maxHealth);

            if(_health <= 0)
            {
                isAlive = false;
                StartCoroutine(Victory());
            }
        } }
    public int maxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;

            if (_health <= 0)
            {
                isAlive = false;
            }
        }
    }
    public bool isAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            anim.SetBool("isAlive", value);
        }
    }
    public bool lockVelocity { 
        get {
            return anim.GetBool("lockVelocity"); 
        } set { 
            anim.SetBool("lockVelocity", value); 
        } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public bool Hit(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            anim.SetTrigger("hit");
            lockVelocity = true;
            damageableHit?.Invoke(damage);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        
        return false;
    }
    private IEnumerator Victory()
    {
        yield return new WaitForSeconds(2.5f);
        GameManager.instance.YouWin();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class SpellCooldown : MonoBehaviour
{
    [Header("UI items for Spell Cooldown")]
    [SerializeField]
    private Image imageCooldown_1;
    [SerializeField]
    private TMP_Text textCooldown_1;
    [SerializeField]
    private Image imageEdge_1;

    [SerializeField]
    private Image imageCooldown_2;
    [SerializeField]
    private TMP_Text textCooldown_2;
    [SerializeField]
    private Image imageEdge_2;

    [SerializeField]
    private Image imageCooldown_3;
    [SerializeField]
    private TMP_Text textCooldown_3;
    [SerializeField]
    private Image imageEdge_3;

    [SerializeField]
    private Image imageCooldown_4;
    [SerializeField]
    private TMP_Text textCooldown_4;
    [SerializeField]
    private Image imageEdge_4;

    [SerializeField]
    private Image imageCooldown_5;
    [SerializeField]
    private Image imageEdge_5;

    private Damageable playerDamageable;

    //variable for looking after the cooldown
    private bool isCoolDown_1 = false;
    private bool isCoolDown_2 = false;
    private bool isCoolDown_3 = false;
    private bool isCoolDown_4 = false;

    private float cooldownTime_1 = 3.0f;
    private float cooldownTime_2 = 4.0f;
    private float cooldownTime_3 = 5.0f;
    private float cooldownTime_4 = 10.0f;
    private float cooldownTimer_1 = 0.0f;
    private float cooldownTimer_2 = 0.0f;
    private float cooldownTimer_3 = 0.0f;
    private float cooldownTimer_4 = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        textCooldown_1.gameObject.SetActive(false);
        textCooldown_2.gameObject.SetActive(false);
        textCooldown_3.gameObject.SetActive(false);
        textCooldown_4.gameObject.SetActive(false);

        imageEdge_1.gameObject.SetActive(false);
        imageEdge_2.gameObject.SetActive(false); 
        imageEdge_3.gameObject.SetActive(false);
        imageEdge_4.gameObject.SetActive(false);
        imageEdge_5.gameObject.SetActive(true);

        imageCooldown_1.fillAmount = 0.0f;
        imageCooldown_2.fillAmount = 0.0f;
        imageCooldown_3.fillAmount = 0.0f;
        imageCooldown_4.fillAmount = 0.0f;
        imageCooldown_5.fillAmount = 1.0f;
        playerDamageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isCoolDown_1)
        {
            ApplyCooldown_1();
        }
        if (isCoolDown_2)
        {
            ApplyCooldown_2();
        }
        if (isCoolDown_3)
        {
            ApplyCooldown_3();
        }
        if (isCoolDown_4)
        {
            ApplyCooldown_4();
        }
        if(playerDamageable.mana == 100)
        {
            ApplyCooldown_5();
        }
    }
    
    public void UIAttack_1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseSpell_1();
        }
    }
    public void UIAttack_2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseSpell_2();
        }
    }
    public void UIAttack_3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseSpell_3();
        }
    }
    public void UIAttack_4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UseSpell_4();
        }
    }
    private void ApplyCooldown_1()
    {
        cooldownTimer_1 -= Time.deltaTime;
        if(cooldownTimer_1 < 0.0f)
        {
            isCoolDown_1 = false;
            textCooldown_1.gameObject.SetActive(false);
            imageEdge_1.gameObject.SetActive(false);
            imageCooldown_1.fillAmount = 0.0f;
        }
        else
        {
            textCooldown_1.text = Mathf.RoundToInt(cooldownTimer_1).ToString();
            imageCooldown_1.fillAmount = cooldownTimer_1 / cooldownTime_1;
        }
    }
    private void ApplyCooldown_2()
    {
        cooldownTimer_2 -= Time.deltaTime;
        if (cooldownTimer_2 < 0.0f)
        {
            isCoolDown_2 = false;
            textCooldown_2.gameObject.SetActive(false);
            imageEdge_2.gameObject.SetActive(false);
            imageCooldown_2.fillAmount = 0.0f;
        }
        else
        {
            textCooldown_2.text = Mathf.RoundToInt(cooldownTimer_2).ToString();
            imageCooldown_2.fillAmount = cooldownTimer_2 / cooldownTime_2;
        }
    }
    private void ApplyCooldown_3()
    {
        cooldownTimer_3 -= Time.deltaTime;
        if (cooldownTimer_3 < 0.0f)
        {
            isCoolDown_3 = false;
            textCooldown_3.gameObject.SetActive(false);
            imageEdge_3.gameObject.SetActive(false);
            imageCooldown_3.fillAmount = 0.0f;
        }
        else
        {
            textCooldown_3.text = Mathf.RoundToInt(cooldownTimer_3).ToString();
            imageCooldown_3.fillAmount = cooldownTimer_3 / cooldownTime_3;
        }
    }
    private void ApplyCooldown_4()
    {
        cooldownTimer_4 -= Time.deltaTime;
        if (cooldownTimer_4 < 0.0f)
        {
            isCoolDown_4 = false;
            textCooldown_4.gameObject.SetActive(false);
            imageEdge_4.gameObject.SetActive(false);
            imageCooldown_4.fillAmount = 0.0f;
        }
        else
        {
            textCooldown_4.text = Mathf.RoundToInt(cooldownTimer_4).ToString();
            imageCooldown_4.fillAmount = cooldownTimer_4 / cooldownTime_4;
        }
    }
    private void ApplyCooldown_5()
    {
        imageCooldown_5.fillAmount = 0.0f;
        imageEdge_5.gameObject.SetActive(false);
    }

    private bool UseSpell_1()
    {
        if(isCoolDown_1)
        {
            return false;
        }
        else
        {
            isCoolDown_1 = true;
            textCooldown_1.gameObject.SetActive(true);
            cooldownTimer_1 = cooldownTime_1;
            textCooldown_1.text = Mathf.RoundToInt(cooldownTimer_1).ToString();
            imageCooldown_1.fillAmount = 1.0f;

            imageEdge_1.gameObject.SetActive(true);
            return true; 
        }
    }
    private bool UseSpell_2()
    {
        if (isCoolDown_2)
        {
            return false;
        }
        else
        {
            isCoolDown_2 = true;
            textCooldown_2.gameObject.SetActive(true);
            cooldownTimer_2 = cooldownTime_2;
            textCooldown_2.text = Mathf.RoundToInt(cooldownTimer_2).ToString();
            imageCooldown_2.fillAmount = 1.0f;

            imageEdge_2.gameObject.SetActive(true);
            return true;
        }
    }
    private bool UseSpell_3()
    {
        if (isCoolDown_3)
        {
            return false;
        }
        else
        {
            isCoolDown_3 = true;
            textCooldown_3.gameObject.SetActive(true);
            cooldownTimer_3 = cooldownTime_3;
            textCooldown_3.text = Mathf.RoundToInt(cooldownTimer_3).ToString();
            imageCooldown_3.fillAmount = 1.0f;

            imageEdge_3.gameObject.SetActive(true);
            return true;
        }
    }
    private bool UseSpell_4()
    {
        if (isCoolDown_4)
        {
            return false;
        }
        else
        {
            isCoolDown_4 = true;
            textCooldown_4.gameObject.SetActive(true);
            cooldownTimer_4 = cooldownTime_4;
            textCooldown_4.text = Mathf.RoundToInt(cooldownTimer_4).ToString();
            imageCooldown_4.fillAmount = 1.0f;

            imageEdge_1.gameObject.SetActive(true);
            return true;
        }
    }
    public void UseSpell_5()
    {
        imageEdge_5.gameObject.SetActive(true);
        imageCooldown_5.fillAmount = 1.0f;
    }
}

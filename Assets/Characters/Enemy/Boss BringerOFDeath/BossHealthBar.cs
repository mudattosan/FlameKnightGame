using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private BossDamageable bossDamageable;

    public Slider slider;
    private void Awake()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Enemy");

        if (boss == null)
        {
            Debug.Log("No boss found");
        }
        else
            bossDamageable = boss.GetComponent<BossDamageable>();
    }
    private void Start()
    {
        slider.value = CalculateSliderHealthValue(bossDamageable.health, bossDamageable.maxHealth);
    }
    private void OnEnable()
    {
        bossDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        bossDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    private float CalculateSliderHealthValue(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
    private void OnPlayerHealthChanged(int newhealth, int maxHealth)
    {
        slider.value = CalculateSliderHealthValue(newhealth, maxHealth);
    }
}

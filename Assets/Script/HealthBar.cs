using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Damageable playerDamageable;

    public Slider slider;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("No player found");
        } else 
            playerDamageable = player.GetComponent<Damageable>();
    }
    private void Start()
    {
        slider.value = CalculateSliderHealthValue(playerDamageable.health, playerDamageable.maxHealth);
    }
    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
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

   

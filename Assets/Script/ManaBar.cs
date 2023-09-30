using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    private Damageable playerDamageable;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }
    private void Start()
    {
        slider.value = CalculateSliderManaValue(playerDamageable.mana, playerDamageable.maxMana);
    }
    private void OnEnable()
    {
        playerDamageable.manaChanged.AddListener(OnPlayerManaChanged);
    }
    private void OnDisable()
    {
        playerDamageable.manaChanged.RemoveListener(OnPlayerManaChanged);
    }
    private float CalculateSliderManaValue(float currentMana, float maxMana)
    {
        return slider.value = currentMana / maxMana;
    }
    private void OnPlayerManaChanged(int newMana, int maxMana)
    {
        slider.value = CalculateSliderManaValue(newMana, maxMana);
    }   
}

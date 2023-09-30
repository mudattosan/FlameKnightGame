using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject manaTextPrefab;

    public Canvas gameCanvas;
    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        if(gameCanvas == null)
        {
            Debug.LogError("No canvas found in scene");
        }
    }
    private void OnEnable()
    {
        CharacterEvents.characterDamaged +=(CharacterTookDamage);
        CharacterEvents.characterHealed += (CharacterHealed);
        CharacterEvents.characterMana += (CharacterMana);
    }
    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= (CharacterTookDamage);
        CharacterEvents.characterHealed -= (CharacterHealed);
        CharacterEvents.characterMana -= (CharacterMana);
    }
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // Create text at character hit
        Vector3 spawnPosition = character.transform.position;

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = character.transform.position;

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
    public void CharacterMana(GameObject character, int manaRecieve)
    {
        Vector3 spawnPosition = character.transform.position;

        TMP_Text tmpText = Instantiate(manaTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = manaRecieve.ToString();
    }
}

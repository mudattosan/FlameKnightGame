using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }
    public int totalEnemies;
    public GameObject obeliskPrefab;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    public void OnEnemyKilled()
    {
        totalEnemies--;
        if(totalEnemies <= 0)
        {
            SpawnObelisk();
        }
    }
    private void SpawnObelisk()
    {
        GameObject obelisk = Instantiate(obeliskPrefab, spawnPosition, spawnRotation);
    }
}

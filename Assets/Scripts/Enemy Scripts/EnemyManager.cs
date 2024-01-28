using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class used to alert enemies when the player is detected
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; set; }

    // List of all enemies alive on the current level
    public List<EnemyAI> enemies;

    // TODO: Mark object as dont destroy on scene change
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate enemy manager detected, deleting it!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    // Add an enemy to the enemy list
    public void AddEnemy(EnemyAI enemyToAdd)
    {
        enemies.Add(enemyToAdd);
    }

    // Alert enemies a player was revealed by stealth
    public void AlertEnemiesPlayerDetected(Vector3 detectionPoint)
    {
        // Do not alert enemies if interactsWithEnemies is false
        if (!PlayerController.Instance.interactsWithEnemies) return;
        if (enemies.Count >= 1)
        {
            foreach (EnemyAI enemy in enemies)
            {
                enemy.AlertPlayerDetected(detectionPoint);
            }
        }
        else
        {
            Debug.Log("No enemies to alert");
        }
    }

    // Update the detection radius of all enemies
    // Used when player goes stealth
    public void SetAllEnemyAlertRadius(float newRadius)
    {
        if (enemies.Count >= 1)
        {
            foreach (EnemyAI enemy in enemies)
            {
                StartCoroutine(enemy.enemyStealthDetection.LerpAlertRadius(newRadius));
            }
        }
    }
}

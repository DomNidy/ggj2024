using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate game manager detected, deleting it!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Trigger player death
    public void LoseGame()
    {
        UIManager.Instance.ShowLoseGameUI();
        PlayerController.Instance.isStealthed = true;
        PlayerController.Instance.canMove = false;
    }
}

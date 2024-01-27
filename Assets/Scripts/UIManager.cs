using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject alertTextPrefab;
    public GameObject uiCanvas;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate ui manager detected, deleting it!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void ShowLoseGameUI()
    {
        Instantiate(alertTextPrefab, uiCanvas.transform);
    }

}

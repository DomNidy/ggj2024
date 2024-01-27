using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // Stealth ability button
    public Button stealthButton;
    // While stealthed, display this prefab over the ui
    public GameObject stealthUIOverlay;

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

    public void ToggleStealthOverlay()
    {
        Instantiate(stealthUIOverlay);

    }

}

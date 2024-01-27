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
    // Instance of the stealth ui overlay (should exist only if we're stealthed)
    private GameObject _stealthUIOverlayInstace;

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
        if (_stealthUIOverlayInstace)
        {
            Debug.Log("Toggled stealth ui off");
            Destroy(_stealthUIOverlayInstace);
        }
        else
        {
            Debug.Log("Toggled stealth ui on");
            _stealthUIOverlayInstace = Instantiate(stealthUIOverlay, uiCanvas.transform);
        }
    }

}

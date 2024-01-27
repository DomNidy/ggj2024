using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertTextScript : MonoBehaviour
{

    public string alertText;
    private TMP_Text tmpAlertText;

    private void Awake()
    {
        tmpAlertText = GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        tmpAlertText.text = alertText;
    }
}

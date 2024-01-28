using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthButton : MonoBehaviour
{
    public UnityEngine.UI.Image stealthIcon;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.isStealthed)
        {
            stealthIcon.color = Color.white;
            stealthIcon.fillAmount = PlayerController.Instance.stealthAbility.remainingStealthCharge / PlayerController.Instance.stealthAbility.maxStealthCharges;
        }
        else
        {
            stealthIcon.color = Color.clear;
        }
    }
}

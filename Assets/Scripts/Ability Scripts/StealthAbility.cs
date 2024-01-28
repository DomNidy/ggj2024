using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthAbility : Ability
{
    // Whether or not the player is able to toggle stealth
    public bool isReady = true;

    // if our stealth charge has been fully depleted
    public bool fullyDepletedStealth = false;

    // Once this reaches zero, the user will be forced out of stealth and can not use it (until they replenish charge)
    public float remainingStealthCharge = 25f;

    // Maximum amount of stealth charges that can be held at once
    public float maxStealthCharges = 25f;

    // The players sprite
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!fullyDepletedStealth)
        {
            TickDepleteStealthCharge();
        }

        if (Input.GetKeyDown(abilityKeybind) && !PlayerController.Instance.isStealthed && isReady && remainingStealthCharge > 0)
            ToggleStealthOn();
        else if (
            Input.GetKeyDown(abilityKeybind)
            && PlayerController.Instance.isStealthed
            && isReady
        )
            ToggleStealthOff();
    }

    private void ToggleStealthOn()
    {
        isReady = false;

        // Update player state to stealthed
        Debug.Log("Stealth toggled on");
        // Make player transparent
        spriteRenderer.color = new Color(255, 255, 255, 0.35f);
        PlayerController.Instance.isStealthed = true;

        EnemyManager.Instance.SetAllEnemyAlertRadius(2f);
        UIManager.Instance.ToggleStealthOverlay();
    }

    private void ToggleStealthOff()
    {
        isReady = false;
        // Update player state to stealthed
        Debug.Log("Stealth toggled off");
        // Make player opaque
        spriteRenderer.color = new Color(255, 255, 255, 1f);
        PlayerController.Instance.isStealthed = false;

        EnemyManager.Instance.SetAllEnemyAlertRadius(5f);
        UIManager.Instance.ToggleStealthOverlay();
    }

    // Deplete remaining stealth charge by 1 for each second it is active
    private void TickDepleteStealthCharge()
    {
        if (PlayerController.Instance.isStealthed)
        {
            Debug.Log("Ticking down");
            remainingStealthCharge -= Time.deltaTime;
        }

        if (remainingStealthCharge <= 0)
        {
            fullyDepletedStealth = true;
            ToggleStealthOff();
        }
    }
}

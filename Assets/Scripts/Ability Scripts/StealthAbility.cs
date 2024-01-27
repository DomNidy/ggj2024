using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthAbility : Ability
{

    // The players sprite
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(abilityKeybind) && !PlayerController.Instance.isStealthed) ToggleStealthOn();
        else if (Input.GetKeyDown(abilityKeybind) && PlayerController.Instance.isStealthed) ToggleStealthOff();

    }
    private void ToggleStealthOn()
    {
        // Update player state to stealthed
        Debug.Log("Stealth toggled on");
        // Make player transparent
        spriteRenderer.color = new Color(255, 255, 255, 0.35f);
        UIManager.Instance.stealthButton.interactable = false;
        PlayerController.Instance.isStealthed = true;

        EnemyManager.Instance.SetAllEnemyAlertRadius(2f);
        UIManager.Instance.ToggleStealthOverlay();
    }
    private void ToggleStealthOff()
    {
        // Update player state to stealthed
        Debug.Log("Stealth toggled off");
        // Make player opaque
        spriteRenderer.color = new Color(255, 255, 255, 1f);
        UIManager.Instance.stealthButton.interactable = false;
        PlayerController.Instance.isStealthed = false;

        EnemyManager.Instance.SetAllEnemyAlertRadius(10f);
        UIManager.Instance.ToggleStealthOverlay();
    }

}

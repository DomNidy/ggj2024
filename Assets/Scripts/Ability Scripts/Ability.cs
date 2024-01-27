using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    // This abilities cooldown
    private float baseCooldown = 5f;

    // Current cd
    private float currentCooldown = 5f;

    // The key which triggers this ability
    public KeyCode abilityKeybind;

}

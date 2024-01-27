using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartLevelButton : MonoBehaviour
{
    private void Start()
    {
        // Add event listener to restart level
        GetComponent<Button>().onClick.AddListener(LevelManager.Instance.RestartLevel);
    }
}

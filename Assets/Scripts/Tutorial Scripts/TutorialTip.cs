using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTip : MonoBehaviour
{
    public GameObject tipPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {

        // If the player enters the collider, trigger tutorial
        if (other.CompareTag("Player"))
        {
            Instantiate(tipPrefab, UIManager.Instance.GetUICanvas().transform);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        // If the player enters the collider, trigger tutorial
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}

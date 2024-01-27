using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStealthDetection : MonoBehaviour
{
    // The radius in which nearby enemy NPCS should be alerted
    public float alertRadius = 5f;

    public CircleCollider2D circleCollider2D;

    // Update is called once per frame
    void Update()
    {
        UpdateDetectionRadius();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        // If this stealth reveal collided with the player, alert the EnemyManger
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Alerting enemy manager");
            Debug.Log(other.transform.position);

            // If the player is not stealthed, alert enemies
            if (!PlayerController.Instance.isStealthed)
            {
                EnemyManager.Instance.AlertEnemiesPlayerDetected(other.transform.position);
            }
        }
    }

    // Update the stealth detection radius
    private void UpdateDetectionRadius()
    {
        transform.localScale = new Vector2(alertRadius, alertRadius);
    }

    public IEnumerator LerpAlertRadius(float newRadius)
    {
        for (
            float currRadius = alertRadius;
            alertRadius > newRadius || alertRadius < newRadius;
            currRadius = Mathf.MoveTowards(alertRadius, newRadius, Time.deltaTime * 30)
        )
        {
            alertRadius = currRadius;
            yield return null;
        }
    }
}

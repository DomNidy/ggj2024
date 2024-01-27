using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public string enemyName;
    public float enemyMovespeed = 2f;
    // When the player is detected, this npc will only be alerted if they're within this distance from the collision point
    public float maxAlertDistance = 10f;

    // Points the player will patrol to
    public Transform[] waypoints;
    // The current waypoint we are travelling to
    public int waypointIndex;
    // Should we traverse the waypoints in reverse? (from the final index to first index)
    // Set to true when the end is reached, set to false when start is reached
    public bool reverseOrder = false;

    // Text above enemy head
    public TMP_Text enemyText;
    private Rigidbody2D rb;

    // Todo: Implement function which causes the npc to travel to the player (if within range)
    void Start()
    {
        EnemyManager.Instance.AddEnemy(this);

        rb = GetComponent<Rigidbody2D>();
        enemyText = GetComponentInChildren<TMP_Text>();
    }


    void Update()
    {
        MoveToWaypoint(waypointIndex);
    }

    /// <summary>
    /// Ran when the enemy manager alert runs
    /// </summary>
    public void AlertPlayerDetected(Vector2 detectionPoint)
    {
        float distanceBetweenDetectionAndPlayer = Vector2.Distance(
            transform.position,
            detectionPoint
        );

        Debug.Log(distanceBetweenDetectionAndPlayer);
        if (distanceBetweenDetectionAndPlayer <= maxAlertDistance)
        {
            enemyText.text = "!!! (" + distanceBetweenDetectionAndPlayer.ToString() + ")";

            // Lose game
            GameManager.Instance.LoseGame();
            return;
        }
        else if (distanceBetweenDetectionAndPlayer <= maxAlertDistance + 2f)
        {
            enemyText.text = "!! (" + distanceBetweenDetectionAndPlayer.ToString() + ")";
            return;
        }
        else if (distanceBetweenDetectionAndPlayer <= maxAlertDistance + 4f)
        {
            enemyText.text = "! (" + distanceBetweenDetectionAndPlayer.ToString() + ")";
            return;
        }
        else
        {
            enemyText.text = "(" + distanceBetweenDetectionAndPlayer.ToString() + ")";
            return;
        }
    }

    private void MoveToWaypoint(int _waypointIndex)
    {
        // Move to the waypoint
        rb.MovePosition(
            Vector2.Lerp(
                transform.position,
                waypoints[_waypointIndex].position,
                enemyMovespeed * Time.deltaTime
            )
        );

        // Check to see if we've reached the current way point, if we have, increment it 
        if (Vector2.Distance(waypoints[waypointIndex].position, transform.position) < 0.2)
        {
            // Are we traversing waypoints in reverse
            if (reverseOrder)
            {
                // If we reach the beginning of the waypoint list, begin traversing to the end again
                if (waypointIndex - 1 < 0)
                {
                    reverseOrder = false;
                    waypointIndex = 1;
                }

                waypointIndex -= 1;
            }
            if (waypointIndex + 1 < waypoints.Length) waypointIndex += 1;
            else
            {
                reverseOrder = true;
                waypointIndex -= 1;
            }
        }
    }
}

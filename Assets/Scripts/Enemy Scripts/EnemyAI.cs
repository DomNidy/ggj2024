using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public string enemyName;
    public float enemyMovespeed = 2f;
    public EnemyStealthDetection enemyStealthDetection;

    // The game object which contains enemy waypoints
    public GameObject waypointGameObj;
    public List<Transform> waypoints = new List<Transform>();
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

        // Get all enemy waypoints in children
        AssignWaypoints();

        enemyStealthDetection = GetComponentInChildren<EnemyStealthDetection>();
        rb = GetComponent<Rigidbody2D>();
        enemyText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        MoveToWaypoint(waypointIndex);
    }

    public void AssignWaypoints()
    {
        Transform[] _waypoints = waypointGameObj.GetComponentsInChildren<Transform>();
        foreach (Transform _waypoint in _waypoints)
        {
            Debug.Log(_waypoint.gameObject.name + " - " + _waypoint.tag);
            // If has matching tag, add it to waypoint array
            if (_waypoint.CompareTag("EnemyPathWaypoint"))
            {
                waypoints.Add(_waypoint);
            }
            else Debug.Log("tag not matching " + _waypoint.gameObject.name);
        }
    }

    /// <summary>
    /// Ran when the enemy manager alert runs
    /// </summary>
    public void AlertPlayerDetected(Vector2 detectionPoint)
    {
        GameManager.Instance.LoseGame();
    }

    private void MoveToWaypoint(int _waypointIndex)
    {
        if (waypoints.Count == 0) return;
        // Move to the waypoint
        rb.MovePosition(
            Vector2.MoveTowards(
                transform.position,
                waypoints[_waypointIndex].transform.position,
                enemyMovespeed * Time.deltaTime
            )
        );

        // Check to see if we've reached the current waypoint 
        if (Vector2.Distance(waypoints[waypointIndex].transform.position, transform.position) < 0.2f)
        {
            // Increment or decrement waypointIndex based on the traversal direction
            if (reverseOrder)
            {
                waypointIndex -= 1;

                if (waypointIndex < 0)
                {
                    reverseOrder = false;
                    waypointIndex = 1;
                }
            }
            else
            {
                waypointIndex += 1;

                if (waypointIndex >= waypoints.Count)
                {
                    reverseOrder = true;
                    waypointIndex -= 1;
                }
            }
        }
    }
}

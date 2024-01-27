using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    private Transform waypointPos;

    private void Awake()
    {
        // Set this waypoint position to the transform
        transform.position = waypointPos.transform.position;
    }

    
}

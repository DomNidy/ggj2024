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

    // The enemies current move direction (used to face sprite)
    public Vector2 moveDir;

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

    public SpriteRenderer spriteRenderer;
    public Sprite forwardSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite backwardSprite;
    public Sprite bottomLeftSprite;
    public Sprite bottomRightSprite;
    public Sprite topRightSprite;
    public Sprite topLeftSprite;


    // Todo: Implement function which causes the npc to travel to the player (if within range)
    void Start()
    {
        EnemyManager.Instance.AddEnemy(this);

        // Get all enemy waypoints in children
        AssignWaypoints();

        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyStealthDetection = GetComponentInChildren<EnemyStealthDetection>();
        rb = GetComponent<Rigidbody2D>();
        enemyText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        FaceEnemySprite();
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

        moveDir = (waypoints[_waypointIndex].transform.position - transform.position).normalized;


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

    // Hacky way to apply the correct sprite to enemy based on movement direction
    private void FaceEnemySprite()
    {

        float inputHorizontal = moveDir.x;
        float inputVertical = moveDir.y;


        if (moveDir.magnitude <= 0.2f) return;

        // Apply bottom left ordinal sprite
        if (inputHorizontal > 0.1f && inputVertical < 0f)
        {
            spriteRenderer.sprite = bottomRightSprite;
            return;
        }
        // Apply bottom right ordinal sprite
        if (inputHorizontal < 0.1f && inputVertical < 0f)
        {
            spriteRenderer.sprite = bottomLeftSprite;
            return;
        }

        // Apply top left ordinal sprite
        if (inputHorizontal < -0.1f && inputVertical > 0f)
        {
            spriteRenderer.sprite = topLeftSprite;
            return;
        }
        // Apply top right ordinal sprite
        if (inputHorizontal > -0.1f && inputVertical > 0f)
        {
            spriteRenderer.sprite = topRightSprite;
            return;
        }

        if (inputHorizontal < 0f)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (inputHorizontal > 0f)
        {
            spriteRenderer.sprite = rightSprite;
        }

        if (inputVertical > 0f)
        {
            spriteRenderer.sprite = backwardSprite;
        }
        else if (inputVertical < 0f)
        {
            spriteRenderer.sprite = forwardSprite;
        }


    }
}

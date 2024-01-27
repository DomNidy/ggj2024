using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public string enemyName;

    public float enemyMovespeed = 2f;

    // When the player is detected, this npc will only be alerted if they're within this distance from the collision point
    public float maxAlertDistance = 10f;



    // Text above enemy head
    public TMP_Text enemyText;
    // Reference to the player gameobject
    public GameObject player;

    private Rigidbody2D rb;

    // This is the floor tilemap which contains walkable tiles
    // Will be used to get cells to perform A*
    // Empty cells will be assumed to be unwalkable
    private Tilemap walkableTilemap;


    // Todo: Implement function which causes the npc to travel to the player (if within range)
    void Start()
    {
        EnemyManager.Instance.AddEnemy(this);

        rb = GetComponent<Rigidbody2D>();
        enemyText = GetComponentInChildren<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Ran when the enemy manager alert runs
    /// </summary>
    public void AlertPlayerDetected(Vector2 detectionPoint)
    {
        float distanceBetweenDetectionAndPlayer = Vector2.Distance(transform.position, detectionPoint);

        Debug.Log(distanceBetweenDetectionAndPlayer);
        if (distanceBetweenDetectionAndPlayer <= maxAlertDistance)
        {
            enemyText.text = "!!! (" + distanceBetweenDetectionAndPlayer.ToString() + ")";
            MoveToPlayer();
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

    // Move to the player
    private void MoveToPlayer()
    {

        BoundsInt bounds = new BoundsInt();
        bounds.SetMinMax(Vector3Int.zero, new Vector3Int(10, 10, 10));
        TileBase[] tiles = walkableTilemap.GetTilesBlock(bounds);

        foreach (TileBase tile in tiles)
        {
            Debug.Log(tile.name);
        }

        rb.MovePosition(Vector2.Lerp(transform.position, player.transform.position, enemyMovespeed * Time.deltaTime));
    }
}

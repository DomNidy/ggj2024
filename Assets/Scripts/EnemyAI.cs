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

    // Bounds to perform A* in
    public Vector2Int scanBounds;

    // Text above enemy head
    public TMP_Text enemyText;

    // Reference to the player gameobject
    public GameObject player;
    // This is the floor tilemap which contains walkable tiles
    // Will be used to get cells to perform A*
    // Empty cells will be assumed to be unwalkable
    public Tilemap walkableTilemap;

    // Current path goal
    public Vector3 pathGoal;
    public List<Vector3Int> path;
    // Nodes around player
    public List<Node> nodes;
    private Rigidbody2D rb;

    // Todo: Implement function which causes the npc to travel to the player (if within range)
    void Start()
    {
        EnemyManager.Instance.AddEnemy(this);

        rb = GetComponent<Rigidbody2D>();
        enemyText = GetComponentInChildren<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        path = new List<Vector3Int>();
        nodes = new List<Node>();
    }


    void Update()
    {
        foreach (Node node in nodes)
        {
            Debug.Log(node.position.x + " " + node.position.y + " " + node.position.z + " node");
        }
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

            // Update path goal to players position since we're in range
            pathGoal = player.transform.position;

            // Lose game
            GameManager.Instance.LoseGame();
            GetTilesAroundEnemy();
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
    // private void MoveToPlayer()
    // {
    //     rb.MovePosition(
    //         Vector2.Lerp(
    //             transform.position,
    //             player.transform.position,
    //             enemyMovespeed * Time.deltaTime
    //         )
    //     );
    // }


    // Get the tiles within the bounds around the enemy
    private void GetTilesAroundEnemy()
    {
        // Get all tiles within 5x5 block around enemy
        for (int i = (int)transform.position.x - scanBounds.x; i < (int)transform.position.x + scanBounds.x; i++)
        {
            for (int j = (int)transform.position.y - scanBounds.y; j < (int)transform.position.y + scanBounds.y; j++)
            {
                TileBase tile = walkableTilemap.GetTile(new Vector3Int(i, j, 0));

                if (tile)
                {
                    walkableTilemap.SetColor(new Vector3Int(i, j, 0), Color.cyan);
                    nodes.Add(new Node(new Vector3Int(i, j, 0)));
                }

            }
        }

        Vector3Int startNodePosition = GetNearestNode(transform.position);
        Vector3Int goalNodePosition = GetNearestNode(pathGoal);

        path = AStar(nodes, startNodePosition, goalNodePosition);
    }

    private Vector3Int GetNearestNode(Vector3 position)
    {
        float minDistance = float.MaxValue;
        Vector3Int nearestNodePosition = Vector3Int.zero;

        foreach (Node node in nodes)
        {
            float distance = Vector3.Distance(position, node.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNodePosition = node.position;
            }
        }

        return nearestNodePosition;
    }

    private List<Vector3Int> AStar(List<Node> nodes, Vector3Int start, Vector3Int goal)
    {
        Debug.Log("Calling a star");
        HashSet<Vector3Int> openSet = new HashSet<Vector3Int>();
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float>();
        Dictionary<Vector3Int, float> fScore = new Dictionary<Vector3Int, float>();

        openSet.Add(start);
        gScore[start] = 0;
        fScore[start] = Vector3.Distance(start, goal);

        while (openSet.Count > 0)
        {
            Vector3Int current = GetLowestFScore(openSet, fScore);
            Debug.Log("Current " + current.ToString());
            openSet.Remove(current);

            if (current == goal)
            {
                return ReconstructPath(cameFrom, current);
            }

            closedSet.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current, nodes))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + Vector3.Distance(current, neighbor);

                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Vector3.Distance(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return new List<Vector3Int>(); // No path found
    }

    private Vector3Int GetLowestFScore(HashSet<Vector3Int> openSet, Dictionary<Vector3Int, float> fScore)
    {
        float lowestFScore = float.MaxValue;
        Vector3Int lowestNode = Vector3Int.zero;

        foreach (Vector3Int node in openSet)
        {
            if (fScore.ContainsKey(node) && fScore[node] < lowestFScore)
            {
                lowestFScore = fScore[node];
                lowestNode = node;
            }
        }

        return lowestNode;
    }

    private List<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        List<Vector3Int> path = new List<Vector3Int> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }

        return path;
    }

    private List<Vector3Int> GetNeighbors(Vector3Int node, List<Node> nodes)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        foreach (Node n in nodes)
        {
            if (Vector3Int.Distance(node, n.position) == 1)
            {
                neighbors.Add(n.position);
            }
        }
        return neighbors;
    }


    // Node class representing a tile in the grid
    [System.Serializable]
    public class Node
    {
        public Vector3Int position;

        public Node(Vector3Int position)
        {
            this.position = position;
        }
    }

}

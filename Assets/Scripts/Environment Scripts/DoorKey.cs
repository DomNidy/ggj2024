using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorKey : MonoBehaviour
{
    public Tilemap wallTilemap;

    public Vector3Int doorPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnlockDoor();
        }
    }

    public void UnlockDoor()
    {
        // If we're more than 5 units away from the door this key is supposed to unlock, do nothing
        if (Vector2.Distance(transform.position, (Vector3)doorPosition) > 5f) return;
        // Delete door
        else
        {
            wallTilemap.SetTile(doorPosition, null);
            // Remove this component
            Destroy(this);
        }

    }
}

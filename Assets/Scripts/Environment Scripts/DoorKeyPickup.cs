using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorKeyPickup : MonoBehaviour
{
    // The position of the door this key is intended to unlock
    public Vector3Int doorPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GivePlayerKey();
    }

    public void GivePlayerKey()
    {
        // If we're within 3 units from the player character
        // Grant them the key, add the pickup script to their gameobj, destroy this gameobject in the world
        if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) < 3)
        {
            DoorKey key = PlayerController.Instance.AddComponent<DoorKey>();
            // Update the doorPosition of the added doorkey script
            key.doorPosition = doorPosition;
            // Set the added doorkey script tilemap to the wall tile map (this is what doors are placed on)
            key.wallTilemap = GameObject.FindGameObjectWithTag("WallTilemap").GetComponent<Tilemap>();
            // Destroy this key gameobj
            Destroy(gameObject);
        }
    }
}

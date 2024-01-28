using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;

    void Start()
    // Start is called before the first frame update
    {
        StartCoroutine(PlaceRandomCloud());
    }

    // Place a cloud in a random position, apply movement
    private IEnumerator PlaceRandomCloud()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            float randomScale = Random.Range(1, 2.5f);

            Instantiate(
                cloudPrefab,
                new Vector3(
                    PlayerController.Instance.transform.position.x + Random.Range(-10, 10),
                    PlayerController.Instance.transform.position.y + Random.Range(-5, 5),
                    0
                ),
                Quaternion.identity,
                PlayerController.Instance.transform
            ).transform.localScale = new Vector3(
                randomScale, randomScale
            );

            Debug.Log(Camera.main.rect + " cam rect");
        }
    }
}

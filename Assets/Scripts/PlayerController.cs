using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Attatch to player game object
public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed = 250f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float inputVertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        // Direction to move player in
        Vector3 moveDirection = new Vector2(inputHorizontal, inputVertical);

        rb.velocity = moveDirection;
    }
}

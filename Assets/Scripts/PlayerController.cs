using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Attatch to player game object
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }
    public Transform spawnPoint;

    public Rigidbody2D rb;
    public float moveSpeed = 250f;
    public bool canMove = true;
    private bool _isStealthed = false;
    // If the enemy can be revealed my stealth detectors
    public bool isStealthed;
    public Sprite forwardSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite backwardSprite;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float inputHorizontal;
    private float inputVertical;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.position = spawnPoint.position;

        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate player controller detected, deleting it!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    void Update()
    {
        FacePlayerSprite();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update the player sprite to the sprite facing the direction they're moving
        if (canMove) Move();
        else moveDirection = Vector2.zero;
        rb.velocity = moveDirection;
    }

    private void Move()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        inputVertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        // Direction to move player in
        moveDirection = new Vector2(inputHorizontal, inputVertical);
    }

    void FacePlayerSprite()
    {
        if (moveDirection.magnitude <= 0.001f) return;


        if (inputHorizontal > 0f)
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (inputHorizontal < 0f)
        {
            spriteRenderer.sprite = leftSprite;
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

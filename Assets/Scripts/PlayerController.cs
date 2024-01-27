using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attatch to player game object
public class PlayerController : MonoBehaviour
{

    private float _moveSpeed = 5f;
    public float moveSpeed
    {
        get
        {
            return _moveSpeed;
        }
        set
        {
            Debug.Log("Setting moveSpeed to " + value.ToString());
            _moveSpeed = value;
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float inputVertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        // Direction to move player in
        Vector3 moveDirection = new Vector3(inputHorizontal, inputVertical);

        transform.position += moveDirection;

    }
}

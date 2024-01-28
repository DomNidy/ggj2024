using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    // This cloud will move to this X position in the world over the course of its lifetime
    private float endPositionX;
    // Start is called before the first frame update
    void Start()
    {
        endPositionX = transform.position.x - 500;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, endPositionX, 0.0001f * Time.deltaTime), transform.position.y, 0);
    }
}

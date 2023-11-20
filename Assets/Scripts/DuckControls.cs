using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckControls : MonoBehaviour
{
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyLeft = KeyCode.A;
    public KeyCode keyDuck = KeyCode.S;
    public KeyCode keyRight = KeyCode.D;
    //radius of pipe
    public float radius = 0.45f;
    //player model
    public Transform duck;
    //gravity factor
    public float gravity = 1;

    public float horizontalSpeed = 1.0f;
    public float jumpForce = 10f;
    private LayerMask groundLayer;
    public bool _isGrounded;
    private float _initialZPos;
    private float bounceForce = 10.0f; // Die Kraft, mit der die Spieler abprallen.
    private Rigidbody rb;
    
    private void Start()
    {
        rb = duck.GetComponent<Rigidbody>();
    }

    void Update() {
        //fall down
        if (duck.position.y <= -radius)
        {
            rb.velocity = Vector3.zero;
        }else
        {
            rb.velocity -= transform.up * gravity;
        }
        
        
        if (Input.GetKey(keyLeft)) {
            transform.Rotate(Vector3.forward,horizontalSpeed);
        }
        if (Input.GetKey(keyRight)) {
            transform.Rotate(Vector3.forward,-horizontalSpeed);

        }
        if (Input.GetKeyDown(keyUp) && duck.position.y <= -radius)
        {
            rb.velocity = transform.up * jumpForce;
        }
        if (Input.GetKey(keyDuck)) {
           
        } else {
         
        }
    }
}

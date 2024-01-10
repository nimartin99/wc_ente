using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckControls : MonoBehaviour
{
    [SerializeField] private GameObject duckModel;
    
    // Keycodes of this player
    [SerializeField] public KeyCode keyUp;
    [SerializeField] public KeyCode keyLeft;
    [SerializeField] public KeyCode keyDuck;
    [SerializeField] public KeyCode keyRight;

    // The left and right (horizontal) speed with which the duck can move in the pipe
    [SerializeField] public float horizontalSpeed = 0.4f;
    
    // The force with which the duck can jump
    [SerializeField] private float jumpForce = 2f;
    
    // Ground layer mask
    [SerializeField] private LayerMask groundLayer;
    
    // The check if the duck is on the ground or not
    public bool _isGrounded;

   
    //the radius of the pipe for grounding
    public float radius = .45f;
    //the duck-child
    public GameObject duck;
    
    // The anchor that holds the duck in the middle
    [SerializeField] private Transform anchor;
    
    // The initial z position of the duck so its always on the same z coordinate
    private float _initialZPos;
    
    // Rigidbody of the duck
    private Rigidbody rb;
    
    // The power with which you push away yourself and enemies when touched
    [SerializeField] private float bounceForce = 10.0f; // Die Kraft, mit der die Spieler abprallen.


    //relative zMovement of duck
    private float yMovement;
    //gravity of duck (jump)
    public float gravityFactor = 0.1f;

    public bool useDrag = false;
    //drag towards bottom of pipe
    public float dragFactor = .1f;
    //the angle in the bottom in which the duck is stable
    public float drag_tolerance = 5;
    public float zRotation;
    public float maxRotationSpeed = 1;

    //the movement resulting from being pushed by other players
    public float pushMovement = 0;
    //the total movement of the player, push movement and normal movement added up
    public float totalMovement = 0;
    //the friction the duck slows down with after being pushed
    public float friction = 1;
    private void Start() {
        // Set the initial z position
        _initialZPos = transform.position.z;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // Reset the z position
        transform.position = new Vector3(transform.position.x, transform.position.y, _initialZPos);

        //_isGrounded = Physics.Raycast(transform.position, transform.position - anchor.position, 0.1f, groundLayer);
        _isGrounded = (duck.transform.localPosition.y <= -radius) && yMovement <= 0;
        //Debug.DrawLine(transform.position,  anchor.position, Color.blue);
        if (Input.GetKey(keyLeft))
        {
            zRotation += horizontalSpeed;
        }
        else if (Input.GetKey(keyRight))
        {
            zRotation -= horizontalSpeed;
        }else if (!useDrag)
        {
            zRotation = 0;
        }
        // Let the player jump when the key is pressed and the player is on the floor
        if (Input.GetKeyDown(keyUp) && _isGrounded) {
            //duck.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
            yMovement = jumpForce;
            _isGrounded = false;
        }
        
        // Scale the duck down while the duck key is pressed (GetKey instead of GetKeyDown) otherwise reset the scale
        if (Input.GetKey(keyDuck)) {
            duckModel.transform.localScale = new Vector3(
                duckModel.transform.localScale.x, 
                duckModel.transform.localScale.y, 
                0.04f
            );
        } else {
            duckModel.transform.localScale = new Vector3(
                duckModel.transform.localScale.x, 
                duckModel.transform.localScale.y, 
                0.1f
            );
        }

        duck.transform.localPosition += Vector3.up * (Time.deltaTime * yMovement);

        if (!_isGrounded)
        {
            //reduce zMovement by gravity
            yMovement -= gravityFactor;
        }else
        {
            yMovement = 0;
            duck.transform.localPosition = Vector3.down * radius;
        }
    //add drag if rotation not zero
    float norm_rotation = transform.rotation.eulerAngles.z % 360;
    if (norm_rotation > drag_tolerance && norm_rotation <= 180)
    {
        //right side of pipe
        zRotation -= dragFactor;
    }else if (norm_rotation < 360 - drag_tolerance && norm_rotation > 180)
    {
        //left side of pipe
        zRotation += dragFactor;
    }
    else
    {
        zRotation = zRotation / 2;
    }
    if (zRotation > maxRotationSpeed)
    {
        zRotation = maxRotationSpeed;
    }

    if (zRotation < -maxRotationSpeed)
    {
        zRotation = -maxRotationSpeed;
    }

    totalMovement = zRotation + pushMovement;
    transform.Rotate(Vector3.forward, totalMovement*Time.deltaTime);
    //some friction
    zRotation *= 0.999f;
    if (pushMovement != 0)
    {
        pushMovement *= friction;
    }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Rigidbody of the other player
            Rigidbody rb2 = collision.gameObject.GetComponent<Rigidbody>();
            if (rb2 != null)
            {
                // Direction of the current player to the other player
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                // Add the power to the current player
                rb.AddForce(-direction * bounceForce, ForceMode.Impulse);
                // Add the power to the other player
                rb2.AddForce(direction * bounceForce, ForceMode.Impulse);
            }
        }
    }
}

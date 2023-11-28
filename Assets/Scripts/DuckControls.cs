using System;
using System.Collections;
using System.Collections.Generic;
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
    
    // The anchor that holds the duck in the middle
    [SerializeField] private Transform anchor;
    
    // The initial z position of the duck so its always on the same z coordinate
    private float _initialZPos;
    
    // Rigidbody of the duck
    private Rigidbody rb;
    
    // The power with which you push away yourself and enemies when touched
    [SerializeField] private float bounceForce = 10.0f; // Die Kraft, mit der die Spieler abprallen.

    private void Start() {
        // Set the initial z position
        _initialZPos = transform.position.z;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // Reset the z position
        transform.position = new Vector3(transform.position.x, transform.position.y, _initialZPos);
        // Check if the player is grounded by casting a ray to the groundlayer
        _isGrounded = Physics.Raycast(transform.position, transform.position - anchor.position, 0.1f, groundLayer);
        // Debug.DrawLine(transform.position,  anchor.position, Color.blue);
        
        // Rotate the player through the pipe when the keys are pressed
        if (Input.GetKey(keyLeft)) {
            anchor.eulerAngles = new Vector3(
                anchor.eulerAngles.x,
                anchor.eulerAngles.y,
                anchor.eulerAngles.z + horizontalSpeed
            );
        }
        if (Input.GetKey(keyRight)) {
            anchor.eulerAngles = new Vector3(
                anchor.eulerAngles.x,
                anchor.eulerAngles.y,
                anchor.eulerAngles.z - horizontalSpeed
            );
        }
        // Let the player jump when the key is pressed and the player is on the floor
        if (Input.GetKeyDown(keyUp) && _isGrounded) {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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

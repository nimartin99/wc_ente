using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckControls : MonoBehaviour
{
    [SerializeField] private GameObject duckModel;
    [SerializeField] public KeyCode keyUp;
    [SerializeField] public KeyCode keyLeft;
    [SerializeField] public KeyCode keyDuck;
    [SerializeField] public KeyCode keyRight;

    [SerializeField] public float horizontalSpeed = 0.4f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private LayerMask groundLayer;
    public bool _isGrounded;

   
    //the radius of the pipe for grounding
    public float radius = .45f;
    //the duck-child
    public GameObject duck;
    
    [SerializeField] private Transform anchor;
    private float _initialZPos;
    private Rigidbody rb;
    
    // The power with which you push away yourself and enemies when touched
    [SerializeField] public float bounceForce = 10.0f; // Die Kraft, mit der die Spieler abprallen.



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
        _initialZPos = transform.position.z;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
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
        if (Input.GetKeyDown(keyUp) && _isGrounded) {
            //duck.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
            yMovement = jumpForce;
            _isGrounded = false;
        }
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb1 = GetComponent<Rigidbody>(); // Rigidbody des aktuellen Spielers
            Rigidbody rb2 = collision.gameObject.GetComponent<Rigidbody>(); // Rigidbody des anderen Spielers
            
            if (rb1 != null && rb2 != null)
            {
                // Richtung vom aktuellen Spieler zum anderen Spieler
                Vector3 direction = (collision.transform.position - transform.position).normalized;

                // Kraft auf den aktuellen Spieler anwenden, um abzuprallen
                rb1.AddForce(-direction * bounceForce, ForceMode.Impulse);

                // Kraft auf den anderen Spieler anwenden, um ebenfalls abzuprallen
                rb2.AddForce(direction * bounceForce, ForceMode.Impulse);
            }
        }
    }
}

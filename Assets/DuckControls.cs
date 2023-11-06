using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckControls : MonoBehaviour
{
    [SerializeField] private GameObject duckModel;
    [SerializeField] private KeyCode keyUp = KeyCode.W;
    [SerializeField] private KeyCode keyLeft = KeyCode.A;
    [SerializeField] private KeyCode keyDuck = KeyCode.S;
    [SerializeField] private KeyCode keyRight = KeyCode.D;

    [SerializeField] private float horizontalSpeed = 0.4f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private LayerMask groundLayer;
    public bool _isGrounded;
    
    [SerializeField] private Transform anchor;
    private float _initialZPos;
    private Rigidbody rb;
    [SerializeField] private float bounceForce = 10.0f; // Die Kraft, mit der die Spieler abprallen.

    private void Start() {
        _initialZPos = transform.position.z;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        transform.position = new Vector3(transform.position.x, transform.position.y, _initialZPos);
        _isGrounded = Physics.Raycast(transform.position, transform.position - anchor.position, 0.1f, groundLayer);
        Debug.DrawLine(transform.position,  anchor.position, Color.blue);
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
        if (Input.GetKeyDown(keyUp) && _isGrounded) {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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

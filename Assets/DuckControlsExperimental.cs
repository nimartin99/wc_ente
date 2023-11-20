using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckControlsExperimental : DuckControls
{
     public KeyCode keyUp;
    public KeyCode keyLeft;
     public KeyCode keyDuck;
     public KeyCode keyRight;
    //radius of pipe
    public float radius = 1;
    //player model
    public Transform duck;

     private float horizontalSpeed = 0.4f;
    private float jumpForce = 2f;
    private LayerMask groundLayer;
    public bool _isGrounded;
    private float _initialZPos;
    private float bounceForce = 10.0f; // Die Kraft, mit der die Spieler abprallen.
    
   private void Start() {
    }

    void Update() {
        if (Input.GetKey(keyLeft)) {
            transform.Rotate(Vector3.forward,horizontalSpeed);
        }
        if (Input.GetKey(keyRight)) {
            transform.Rotate(Vector3.forward,-horizontalSpeed);

        }
        if (Input.GetKeyDown(keyUp) && _isGrounded) {
        }
        if (Input.GetKey(keyDuck)) {
           
        } else {
         
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

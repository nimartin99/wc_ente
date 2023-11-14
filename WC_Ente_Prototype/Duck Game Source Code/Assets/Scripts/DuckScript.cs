using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckScript : MonoBehaviour
{
    public Transform centerPoint;
    public GameObject Manager;

    public float moveSpeed = 5f;  // Adjust this value to set the movement speed

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)){
            transform.rotation = Quaternion.Euler(0f, 180f, 315f);
        }

        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)){
            transform.rotation = Quaternion.Euler(0f, 0f, 315f);
        }

        else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)){
            transform.rotation = Quaternion.Euler(0f, 180f, 45f);
        }
        
        else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)){
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }

        else if(Input.GetKey(KeyCode.S)){
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if(Input.GetKey(KeyCode.W)){
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else if(Input.GetKey(KeyCode.A)){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    private void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Pipe"){
            Manager.GetComponent<GameManager>().GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "ScorePoint"){
            other.gameObject.SetActive(false);
            Manager.GetComponent<GameManager>().score++;
        }
    }
}

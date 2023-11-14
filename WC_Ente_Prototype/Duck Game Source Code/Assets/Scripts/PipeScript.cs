using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to set the speed of movement

    void Update()
    {
        // Move the GameObject downward in the Y-axis
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if(transform.position.y < -10f){
            Destroy(gameObject);
        }
    }
}

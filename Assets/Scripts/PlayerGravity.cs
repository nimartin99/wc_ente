using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{

    public float radius = .44f;
    public float gravity_factor = .1f;
    private Rigidbody body;

    private void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (transform.localPosition.y > -radius)
        {
            body.AddRelativeForce(Vector3.down * gravity_factor);
        }
        else
        { 
            body.velocity = Vector3.zero;
           transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public DuckControls myDuck;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("collision with obstacle");
        }
        if (other.gameObject.CompareTag("Player"))
        {
            DuckControls opponent =  other.gameObject.GetComponentInParent<DuckControls>();
            myDuck.pushMovement = opponent.zRotation * opponent.bounceForce;
            opponent.pushMovement = myDuck.zRotation * myDuck.bounceForce;
        }
        
    }
}

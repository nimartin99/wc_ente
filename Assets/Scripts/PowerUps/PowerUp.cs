using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    // The speed with which the powerup moves towards the players
    [SerializeField] private float powerUpFloatSpeed;
    // The duration of the powerup
    [SerializeField] private int duration;
    
    private void Update() {
        Vector3 pos = transform.position;
        // Move the powerup
        transform.position = new Vector3(pos.x, pos.y, pos.z + powerUpFloatSpeed * Time.deltaTime);
    }
    
    /// <summary>
    /// OnTriggerEnter is called, when the powerup hits another collider
    /// </summary>
    /// <param name="other">The hit collider</param>
    private void OnTriggerEnter(Collider other) {
        // Check if the collider is from a player
        if (other.gameObject.CompareTag("Player")) {
            HidePowerUp();
            PowerUpCollected(other.gameObject);
        }
    }

    /// <summary>
    /// This should be called from the child script after the powerup specific PowerUpCollected method was executed.
    /// It starts a coroutine with the child PowerUpElapsed method.
    /// </summary>
    /// <param name="hit">The player gameobject that was hit</param>
    protected virtual void PowerUpCollected(GameObject hit) {
        StartCoroutine(PowerUpElapsed(hit, duration));
    }

    /// <summary>
    /// This should be called from the child script after the powerup specific PowerUpElapsed method was executed
    /// </summary>
    /// <param name="hit">The player gameobject that was hit</param>
    /// <param name="duration">The duration of the powerup (necessary for child methods)</param>
    /// <returns></returns>
    protected virtual IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        // Destroys the gameobject
        Destroy(gameObject);
        // Should never be called because the gameobject is destroyed already but needs a return statment
        return null;
    }

    /// <summary>
    /// Hide the powerup by disabling SphereCollider and MeshRenderer
    /// </summary>
    private void HidePowerUp() {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

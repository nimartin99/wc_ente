using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField] private float powerUpRotationSpeed;
    [SerializeField] public int duration;
    public GameObject particles;
    private void Update() {
        MovePowerup();
        transform.Rotate(Vector3.up * (powerUpRotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            HidePowerUp();
            PowerUpCollected(other.gameObject);
        }
    }

    protected virtual void PowerUpCollected(GameObject hit) {
        AudioManager.Instance.PlayPickup();
        StartCoroutine(PowerUpElapsed(hit, duration));
    }

    protected virtual IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        Destroy(gameObject);
        return null;
    }

    private void HidePowerUp() {
        
        gameObject.GetComponent<SphereCollider>().enabled = false;
        /*
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            MeshRenderer meshRenderer = gameObject.transform.GetChild(i).GetComponent<MeshRenderer>();
            if (meshRenderer != null) {
                gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
        }
        */
        if (GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++) {
                MeshRenderer meshRenderer = gameObject.transform.GetChild(i).GetComponent<MeshRenderer>();
                if (meshRenderer != null) {
                    gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                }
            } 
        }
        
        Instantiate(particles, transform.position, transform.rotation);
        //Destroy(gameObject);
        
    }

    private void MovePowerup() {
        // We could add something here to move the powerup through the pipe by using the bezier curves
    }
}

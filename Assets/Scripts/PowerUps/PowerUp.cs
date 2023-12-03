using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField] private float powerUpFloatSpeed;
    [SerializeField] public int duration;
    
    private void Update() {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.z + powerUpFloatSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            HidePowerUp();
            PowerUpCollected(other.gameObject);
        }
    }

    protected virtual void PowerUpCollected(GameObject hit) {
        StartCoroutine(PowerUpElapsed(hit, duration));
    }

    protected virtual IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        Destroy(gameObject);
        return null;
    }

    private void HidePowerUp() {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

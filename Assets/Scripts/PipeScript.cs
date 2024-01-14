using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour {
    public static PipeScript Instance { get; private set; }
    // The amount of force with which the ducks are pulled towards the pipe
    [SerializeField] private float gravity;
    
    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
    }

    public void Attract(Transform attractTransform) {
        Vector3 gravityDown = (transform.position - attractTransform.position).normalized;
        gravityDown.z = 0;
        attractTransform.GetComponent<Rigidbody>().AddForce(gravityDown * gravity);

        Vector3 localUp = attractTransform.up;
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityDown) * attractTransform.rotation;
        attractTransform.rotation = Quaternion.Slerp(attractTransform.rotation, targetRotation, 100f * Time.deltaTime);
    }
}

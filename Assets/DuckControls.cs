using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckControls : MonoBehaviour {
    [SerializeField] private GameObject duckModel;
    [SerializeField] private KeyCode keyLeft;
    [SerializeField] private KeyCode keyRight;
    [SerializeField] private KeyCode keyDuck;
    [SerializeField] private KeyCode keyUp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyLeft)) {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z + 0.4f
            );
        }
        if (Input.GetKey(keyRight)) {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                transform.eulerAngles.z - 0.4f
            );
        }
        if (Input.GetKeyDown(keyUp)) {
            
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
}

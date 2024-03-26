using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField]
    float delay = 15;
    float start = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        start += Time.deltaTime;
        if(start > delay)
        {
            Destroy(gameObject);
        }
    }
}

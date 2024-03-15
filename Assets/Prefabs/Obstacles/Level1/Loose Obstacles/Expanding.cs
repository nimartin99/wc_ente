using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expanding : MonoBehaviour
{
    float sizeup = 0;
    bool expand = true;
    float delay = 6;
    float start = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (expand && start > delay)
        {
            sizeup = Time.deltaTime * 0.1f;
            transform.localScale += new Vector3(sizeup, sizeup, 0);
            if(transform.localScale.x > 0.18f)
            {
                expand = false;
            }
        }
        start+=Time.deltaTime;
        

    }
}

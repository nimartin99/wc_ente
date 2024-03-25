using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplication : MonoBehaviour
{
    public float delay;
    private float start=0;
    bool startcounting = false;
    Vector3 pos;

    void Update()
    {
        if (startcounting)
        {
            if (start > delay)
            {
                transform.GetComponent<Rigidbody>().AddExplosionForce(100, pos, 2);
                //transform.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 30);
                startcounting = false;
            }
            start += Time.deltaTime;
        }
        


    }

    public void AlterDelay(float d, Vector3 p)
    {
        delay = 7; // 1 + 10 * d;
        pos = p;
        startcounting = true;
    }
}

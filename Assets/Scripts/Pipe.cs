using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
    public float pipeLength;
    public PipeGenerator.PipeType pipeType;
    public float radius = 8;

    [SerializeField] private Transform[] bezierControlPoints;
    private Vector3 gizmosPosition;

    private void OnDrawGizmos() {
        for (float t = 0; t <= 1; t += 0.02f)
        {
            gizmosPosition = MoveAlong(t*pipeLength);
            Gizmos.DrawSphere(gizmosPosition, 0.05f);
        }

        // Gizmos.DrawLine(new Vector3(bezierControlPoints[0].position.x, bezierControlPoints[0].position.y),
        //     new Vector3(bezierControlPoints[1].position.x, bezierControlPoints[1].position.y));
        // Gizmos.DrawLine(new Vector3(bezierControlPoints[2].position.x, bezierControlPoints[2].position.y),
        //     new Vector3(bezierControlPoints[3].position.x, bezierControlPoints[3].position.y));
        
        //Gizmos.DrawLine(bezierControlPoints[0].position, bezierControlPoints[1].position);
        //Gizmos.DrawLine(bezierControlPoints[2].position, bezierControlPoints[3].position);
    }
    
    
    public Vector3 MoveAlong(float t)
    {
        // Ensure t is within the valid range [0, 1]
        t = t / pipeLength;
        t = Mathf.Clamp01(t);

        // Calculate the angle based on t (quarter-circle: 0 to 90 degrees)
        float angle = (1-t) * 90f - 90;

        // Convert angle to radians
        float radians = Mathf.Deg2Rad * angle;
        float x = 0;
        float y = 0;
        float z = 0;

        if (pipeType == PipeGenerator.PipeType.CurveRight)
        {
            // Calculate the position on the quarter-circle with respect to the starting point p
            x = + radius * Mathf.Cos(radians) - radius;
            z = + radius * Mathf.Sin(radians);

            // Ensure the y value is always 0 (on the x-z plane)
            y = 0f;
        }else if (pipeType == PipeGenerator.PipeType.CurveLeft)
        {
            // Calculate the position on the quarter-circle with respect to the starting point p
            x = - radius * Mathf.Cos(radians) + radius;
            z =  + radius * Mathf.Sin(radians);

            // Ensure the y value is always 0 (on the x-z plane)
            y = 0f;
        }else if (pipeType == PipeGenerator.PipeType.Straight)
        {
            x = 0;
            z = -t*pipeLength;

            // Ensure the y value is always 0 (on the x-z plane)
            y = 0f;
        }

        Vector3 output = new Vector3(x, y, z);

        return transform.TransformVector(output)+ transform.position;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
    public float pipeLength;
    public PipeGenerator.PipeType pipeType;

    [SerializeField] private Transform[] bezierControlPoints;
    private Vector3 gizmosPosition;

    private void OnDrawGizmos() {
        for (float t = 0; t <= 1; t += 0.02f) {
            gizmosPosition = Mathf.Pow(1 - t, 3) * bezierControlPoints[0].position +
                             3 * Mathf.Pow(1 - t, 2) * t * bezierControlPoints[1].position +
                             3 * (1 - t) * Mathf.Pow(t, 2) * bezierControlPoints[2].position +
                             Mathf.Pow(t, 3) * bezierControlPoints[3].position;
            Gizmos.DrawSphere(gizmosPosition, 0.05f);
        }

        // Gizmos.DrawLine(new Vector3(bezierControlPoints[0].position.x, bezierControlPoints[0].position.y),
        //     new Vector3(bezierControlPoints[1].position.x, bezierControlPoints[1].position.y));
        // Gizmos.DrawLine(new Vector3(bezierControlPoints[2].position.x, bezierControlPoints[2].position.y),
        //     new Vector3(bezierControlPoints[3].position.x, bezierControlPoints[3].position.y));
        
        Gizmos.DrawLine(bezierControlPoints[0].position, bezierControlPoints[1].position);
        Gizmos.DrawLine(bezierControlPoints[2].position, bezierControlPoints[3].position);
    }
}

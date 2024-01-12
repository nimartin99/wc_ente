using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PipeGenerator : MonoBehaviour {
    public static PipeGenerator Instance { get; private set; }
    
    [SerializeField] private Transform straightPipe;
    [SerializeField] private Transform curveRightPipe;
    [SerializeField] private Transform curveLeftPipe;

    private float singlePipeProgress = 0;
    public Vector3 currentEndPoint;
    private float currentEndRotation;
    
    private PipeType[] debugPipe = new[] { PipeType.Straight, PipeType.CurveLeft, PipeType.CurveRight };
    private int index;
    
    public enum PipeType {
        Straight,
        CurveLeft,
        CurveRight
    };

    public Transform[] currentPipes = new Transform[5];
    public GameObject objectToMove;
    
    private void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Spawn 3 straight pipes
        for (int i = 0; i < currentPipes.Length; i++) {
            currentPipes[i] = Instantiate(straightPipe, new Vector3(0, 0, -4.0f * i), Quaternion.Euler(180, 0, 0), transform);
        }
        currentEndPoint = new Vector3(0, 0, -20.0f);
        currentEndRotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToMove) {
            MoveObjectThroughPipes(objectToMove);
        }
        
        if (singlePipeProgress > currentPipes[0].GetComponent<Pipe>().pipeLength) {
            singlePipeProgress = 0;
            Destroy(currentPipes[0].gameObject);
            for (int j = 0; j < currentPipes.Length - 1; j++) {
                currentPipes[j] = currentPipes[j + 1];
            }
            
            currentPipes[currentPipes.Length - 1] = GenerateNextPipe();
        } 
    }
    
    private void MoveObjectThroughPipes(GameObject obj) {
        float moveValue = 2.0f * Time.deltaTime;
        singlePipeProgress += moveValue;
        
        // Calculate the position on the Bezier curve
        Vector3 newPosition = BezierCurve(singlePipeProgress / currentPipes[0].GetComponent<Pipe>().pipeLength, 
            currentPipes[0].transform.GetChild(0).position, 
            currentPipes[0].transform.GetChild(1).position, 
            currentPipes[0].transform.GetChild(2).position, 
            currentPipes[0].transform.GetChild(3).position);
        
        // Move the object to the new position
        obj.transform.position = newPosition;
        
        // Adjust the rotation angle of the object based on the next step in the Bezier curve
        Vector3 nextPosition = BezierCurve(
            (singlePipeProgress + moveValue) / currentPipes[0].GetComponent<Pipe>().pipeLength,
            currentPipes[0].transform.GetChild(0).position,
            currentPipes[0].transform.GetChild(1).position,
            currentPipes[0].transform.GetChild(2).position,
            currentPipes[0].transform.GetChild(3).position);

        Vector3 direction = (nextPosition - newPosition).normalized;
        if (direction != Vector3.zero) {
            obj.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private Transform GenerateNextPipe() {
        Debug.Log("Generate new Pipe now");
        PipeType pipeType = (PipeType) Random.Range(0, System.Enum.GetValues(typeof(PipeType)).Length);
        // PipeType pipeType = debugPipe[index];
        if (index == 2) {
            index = 0;
        } else {
            index++;
        }
        
        Transform nextPipe;
        if (pipeType == PipeType.CurveRight) {
            nextPipe = Instantiate(curveRightPipe, transform);
            nextPipe.position = currentEndPoint + transform.position;
            nextPipe.rotation = Quaternion.Euler(0, 0 + currentEndRotation, 90);
            currentEndPoint += 5.5f * ConvertAngleToVector2(currentEndRotation);
            currentEndRotation += 90;
        } else if (pipeType == PipeType.CurveLeft) {
            nextPipe = Instantiate(curveLeftPipe, transform);
            nextPipe.position = currentEndPoint + transform.position;
            nextPipe.rotation = Quaternion.Euler(0, 0 + currentEndRotation, -90);
            currentEndPoint += 5.5f * ConvertAngleToVector3(currentEndRotation);
            currentEndRotation -= 90;
        } else {
            nextPipe = Instantiate(straightPipe, transform);
            nextPipe.position = currentEndPoint + transform.position;
            nextPipe.rotation = Quaternion.Euler(180, 0 + currentEndRotation, 0);
            currentEndPoint += 4f * ConvertAngleToVector1(currentEndRotation);
        }
        return nextPipe;
    }
    
    public static Vector3 BezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
    
    // Method 1
    public Vector3 ConvertAngleToVector1(float angle) {
        angle = ((angle % 360) + 360) % 360;
        if (angle == 0 || angle == 360)
        {
            return new Vector3(0, 0, -1);
        }
        else if (angle == 90)
        {
            return new Vector3(-1, 0, 0);
        }
        else if (angle == 180)
        {
            return new Vector3(0, 0, 1);
        }
        else if (angle == 270)
        {
            return new Vector3(1, 0, 0);
        }
        else
        {
            return Vector3.zero;
        }
    }

    // Method 2
    public Vector3 ConvertAngleToVector2(float angle)
    {
        angle = ((angle % 360) + 360) % 360;
        if (angle == 0 || angle == 360)
        {
            return new Vector3(-1, 0, -1);
        }
        else if (angle == 90)
        {
            return new Vector3(-1, 0, 1);
        }
        else if (angle == 180)
        {
            return new Vector3(1, 0, 1);
        }
        else if (angle == 270)
        {
            return new Vector3(1, 0, -1);
        }
        else
        {
            return Vector3.zero;
        }
    }

    // Method 3
    public Vector3 ConvertAngleToVector3(float angle)
    {
        angle = ((angle % 360) + 360) % 360;
        if (angle == 0 || angle == 360)
        {
            return new Vector3(1, 0, -1);
        }
        else if (angle == 90)
        {
            return new Vector3(-1, 0, -1);
        }
        else if (angle == 180)
        {
            return new Vector3(-1, 0, 1);
        }
        else if (angle == 270)
        {
            return new Vector3(1, 0, 1);
        }
        else
        {
            return Vector3.zero;
        }
    }
}

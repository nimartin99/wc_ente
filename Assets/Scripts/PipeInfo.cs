using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PipeInfo", order = 1)]
public class PipeInfo : ScriptableObject {
    public enum PipeType {
        Straight,
        CurveLeft,
        CurveRight
    };
    
    public float pipeLength;
    public PipeType pipeType;
}


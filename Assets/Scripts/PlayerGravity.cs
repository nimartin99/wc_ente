using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour {
    [SerializeField] private PipeScript attractor;

    private void Awake() {
        attractor = PipeScript.Instance;
    }
    
    private void FixedUpdate() {
        if (attractor) {
            attractor.Attract(transform);
        }
    }
}

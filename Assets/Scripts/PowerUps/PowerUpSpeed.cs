using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp {
    public float speedFactor = 2;
    
    protected override void PowerUpCollected(GameObject hit) {
        DuckControls duckControls = hit.GetComponent<DuckControls>();
        if (duckControls) {
            duckControls.horizontalSpeed *= speedFactor;
            base.PowerUpCollected(hit);
        }
    }
    
    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        yield return new WaitForSeconds(duration);
        DuckControls duckControls = hit.GetComponent<DuckControls>();
        if (duckControls) {
            duckControls.horizontalSpeed /= speedFactor;
            base.PowerUpElapsed(hit, duration);
        }
    } 
}

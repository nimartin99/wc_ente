using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp {
    // The factor by which the player gets faster
    public float speedFactor = 2;
    
    /// <summary>
    /// Speeds the player up by the given speedFactor
    /// </summary>
    /// <param name="hit">The player gameobject that was hit</param>
    protected override void PowerUpCollected(GameObject hit) {
        DuckControls duckControls = hit.GetComponent<DuckControls>();
        if (duckControls) {
            duckControls.horizontalSpeed *= speedFactor;
            base.PowerUpCollected(hit);
        }
    }
    
    /// <summary>
    /// Waits for the given duration and then slows the player down again
    /// </summary>
    /// <param name="hit">The player gameobject that was hit</param>
    /// <param name="duration">The duration after which the player gets slowed down</param>
    /// <returns></returns>
    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        yield return new WaitForSeconds(duration);
        DuckControls duckControls = hit.GetComponent<DuckControls>();
        if (duckControls) {
            duckControls.horizontalSpeed /= speedFactor;
            base.PowerUpElapsed(hit, duration);
        }
    } 
}

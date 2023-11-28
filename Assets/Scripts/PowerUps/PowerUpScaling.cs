using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScaling : PowerUp {
    // The factor by which the player duck gets scaled when collected
    public float scalingFactor = 2;
    
    /// <summary>
    /// Scales the player that picked it up by the given scalingFactor
    /// </summary>
    /// <param name="hit">The player gameobject that was hit</param>
    protected override void PowerUpCollected(GameObject hit) {
        Vector3 scale = hit.gameObject.transform.localScale;
        hit.gameObject.transform.localScale = new Vector3(scale.x * scalingFactor, scale.y * scalingFactor, scale.z * scalingFactor);
        
        // Call the parent PowerUpCollected method
        base.PowerUpCollected(hit);
    }
    
    /// <summary>
    /// Waits for the given duration and then scales the player duck back
    /// </summary>
    /// <param name="hit">The player gameobject that was hit</param>
    /// <param name="duration">The duration after which the player duck gets scaled back</param>
    /// <returns></returns>
    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        yield return new WaitForSeconds(duration);
        Vector3 scale = hit.gameObject.transform.localScale;
        hit.gameObject.transform.localScale = new Vector3(scale.x / scalingFactor, scale.y / scalingFactor, scale.z / scalingFactor);
        
        // Call the parent PowerUpElapsed method
        base.PowerUpElapsed(hit, duration);
    } 
}

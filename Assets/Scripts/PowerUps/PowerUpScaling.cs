using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScaling : PowerUp {
    public float scalingFactor = 2;
    
    protected override void PowerUpCollected(GameObject hit) {
        Vector3 scale = hit.gameObject.transform.localScale;
        hit.gameObject.transform.localScale = new Vector3(scale.x * scalingFactor, scale.y * scalingFactor, scale.z * scalingFactor);
        
        base.PowerUpCollected(hit);
    }
    
    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        yield return new WaitForSeconds(duration);
        Vector3 scale = hit.gameObject.transform.localScale;
        hit.gameObject.transform.localScale = new Vector3(scale.x / scalingFactor, scale.y / scalingFactor, scale.z / scalingFactor);
        base.PowerUpElapsed(hit, duration);
    } 
}

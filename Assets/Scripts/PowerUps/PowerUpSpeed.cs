using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUpSpeed : PowerUp {
    public float speedFactor = 2;
    public Sprite SpeedUp;
    public Image PowerUpTarget;
    private void OnEnable()
    {
        PowerUpTarget = GameObject.Find("PowerPikcup").GetComponent<Image>();
    }
    protected override void PowerUpCollected(GameObject hit) {
        PowerUpTarget.GetComponent<Image>().enabled = true;
        PowerUpTarget.sprite = SpeedUp;
        DuckControls duckControls = hit.GetComponentInParent<DuckControls>();
        if (duckControls) {
            duckControls.horizontalSpeed *= speedFactor;
            duckControls.maxRotationSpeed *= speedFactor;
            base.PowerUpCollected(hit);
            duckControls.isSpeedActive = true;
        }
    }
    
    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration) {
        yield return new WaitForSeconds(duration);
        PowerUpTarget.GetComponent<Image>().enabled = false;
        DuckControls duckControls = hit.GetComponentInParent<DuckControls>();
        if (duckControls) {
            duckControls.horizontalSpeed /= speedFactor;
            duckControls.maxRotationSpeed /= speedFactor;
            base.PowerUpElapsed(hit, duration);
            duckControls.isSpeedActive = false;
        }
    } 
}

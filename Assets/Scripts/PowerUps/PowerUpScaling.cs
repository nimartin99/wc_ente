using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpScaling : PowerUp
{
    public Sprite ScaleUp;
    public Image PowerUpTarget;

    private float scalingFactor; // Variable to store the randomly chosen scaling factor

    private void OnEnable()
    {
        PowerUpTarget = GameObject.Find("PowerPikcup").GetComponent<Image>();
    }

    protected override void PowerUpCollected(GameObject hit)
    {
        PowerUpTarget.enabled = true;
        PowerUpTarget.sprite = ScaleUp;

        // Randomly choose between 2x and 0.5x scaling
        scalingFactor = Random.Range(0, 2) > 0 ? 2f : 0.5f;

        Vector3 scale = hit.gameObject.transform.localScale;
        hit.gameObject.transform.localScale = new Vector3(scale.x * scalingFactor, scale.y * scalingFactor, scale.z * scalingFactor);

        base.PowerUpCollected(hit);
    }

    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration)
    {
        yield return new WaitForSeconds(duration);

        PowerUpTarget.enabled = false;
        Vector3 scale = hit.gameObject.transform.localScale;
        hit.gameObject.transform.localScale = new Vector3(scale.x / scalingFactor, scale.y / scalingFactor, scale.z / scalingFactor);

        base.PowerUpElapsed(hit, duration);
    }
}

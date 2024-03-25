using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpScaling : PowerUp
{
    public Sprite ScaleUp;
    public Image PowerUpTarget;

    [SerializeField] private float scalingFactor;

    private void OnEnable()
    {
        PowerUpTarget = GameObject.Find("PowerPikcup").GetComponent<Image>();
    }

    protected override void PowerUpCollected(GameObject hit)
    {
        PowerUpTarget.enabled = true;
        PowerUpTarget.sprite = ScaleUp;

        Vector3 scale = hit.gameObject.transform.localScale;
        hit.transform.parent.GetComponent<DuckControls>().yScale = scale.y * scalingFactor;
        hit.gameObject.transform.localScale = new Vector3(scale.x * scalingFactor, scale.y * scalingFactor, scale.z * scalingFactor);
        Debug.Log("Scale " + hit.gameObject + " by " +  scalingFactor + " so it yields " 
                  + hit.gameObject.transform.localScale);

        base.PowerUpCollected(hit);
    }

    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration)
    {
        yield return new WaitForSeconds(duration);

        PowerUpTarget.enabled = false;
        Vector3 scale = hit.gameObject.transform.localScale;
        hit.transform.parent.GetComponent<DuckControls>().yScale = 1f;
        hit.gameObject.transform.localScale = new Vector3(scale.x / scalingFactor, scale.y / scalingFactor, scale.z / scalingFactor);

        base.PowerUpElapsed(hit, duration);
    }
}

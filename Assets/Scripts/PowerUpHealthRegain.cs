using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHealthRegain : PowerUp
{
    public Sprite HealthRegainSprite; // Sprite to display when the health regain power-up is active
    public Image PowerUpTarget; // UI element to display the power-up status
    public int healthRegainFrequency = 5; // Frequency of health regain within the duration

    private void OnEnable()
    {
        PowerUpTarget = GameObject.Find("PowerPikcup").GetComponent<Image>(); // Find and assign the UI element
    }

    private void Start()
    {
        duration = 5; // Set the duration value inherited from the base class
    }

    protected override void PowerUpCollected(GameObject hit)
    {
        DamageController damageController = hit.GetComponent<DamageController>();
        if (damageController != null)
        {
            PowerUpTarget.enabled = true;
            PowerUpTarget.sprite = HealthRegainSprite;
            StartCoroutine(RegainHealth(hit));
            base.PowerUpCollected(hit);
        }
    }
    private IEnumerator RegainHealth(GameObject hit)
    {
        DamageController damageController = hit.GetComponent<DamageController>();
        if (damageController == null) yield break;

        for (int i = 0; i < healthRegainFrequency; i++)
        {
            damageController.CollectHealth(); // Call the method without passing an argument
            yield return new WaitForSeconds((float)duration / healthRegainFrequency);
        }

        PowerUpTarget.enabled = false; // Hide the UI element
    }

    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration)
    {
        // This method might not be necessary for health regain and could be left empty or removed
        yield return null;
    }
}

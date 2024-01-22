using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpShield : PowerUp
{
    public Sprite ShieldSprite; // Sprite to display when the shield power-up is active
    public Image PowerUpTarget; // UI element to display the power-up status
    public int shieldDuration = 10; // Duration for which the shield is active

    private void OnEnable()
    {
        PowerUpTarget = GameObject.Find("PowerPikcup").GetComponent<Image>(); // Find and assign the UI element
    }
    private void Start()
    {
        duration = shieldDuration;
    }
    protected override void PowerUpCollected(GameObject hit)
    {
        PowerUpTarget.enabled = true;
        PowerUpTarget.sprite = ShieldSprite;

        DamageController damageController = hit.GetComponent<DamageController>();
        if (damageController != null)
        {
            damageController.toogleShield(true);
            StartCoroutine(ShieldDuration(hit, shieldDuration));
            base.PowerUpCollected(hit);
        }
    }

    private IEnumerator ShieldDuration(GameObject hit, int duration)
    {
        yield return new WaitForSeconds(duration);

        DamageController damageController = hit.GetComponent<DamageController>();
        if (damageController != null)
        {
            damageController.toogleShield(false);

        }

        PowerUpTarget.enabled = false; // Hide the UI element
        base.PowerUpElapsed(hit, duration);
    }

    protected override IEnumerator PowerUpElapsed(GameObject hit, int duration)
    {
        // This method might not be necessary for the shield and could be left empty or removed
        yield return null;
    }
}

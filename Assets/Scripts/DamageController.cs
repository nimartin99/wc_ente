using System.Collections;
using UnityEngine;
using TMPro; // Include the TextMesh Pro namespace

public class DamageController : MonoBehaviour
{

    public DuckControls myDuck;

    public float health = 100f; // Initial health
    private Coroutine regainHealthCoroutine; // Coroutine for regaining health over time

    public TextMeshPro healthText; // Reference to the TextMesh Pro text component

    public bool isShieldActive = false; // To check if the shield is active

    private void OnEnable()
    {
       // healthText = GetComponentInChildren<TextMeshProUGUI>(); // Find and assign the TextMesh Pro component

        UpdateHealthUI(); // Update the health UI when the object is enabled
    }

    private void Update()
    {
        //if (health < 100 && regainHealthCoroutine == null)
        //{
        //    regainHealthCoroutine = StartCoroutine(RegainHealth());
        //}
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !isShieldActive)
        {
            Debug.Log("Collision with obstacle");
            health = Mathf.Max(health - 10, 0); // Decrease health but not below 0
            UpdateHealthUI(); // Update the health UI

            if (regainHealthCoroutine != null)
            {
                StopCoroutine(regainHealthCoroutine);
                regainHealthCoroutine = null;
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            DuckControls opponent =  other.gameObject.GetComponentInParent<DuckControls>();
            myDuck.pushMovement = opponent.zRotation * opponent.bounceForce;
            opponent.pushMovement = myDuck.zRotation * myDuck.bounceForce;
        }
    }

    private IEnumerator RegainHealth()
    {
        while (health < 100)
        {
            health = Mathf.Min(health + 0.2f, 100f); // Regain health but not above 100
            UpdateHealthUI(); // Update the health UI
            yield return new WaitForSeconds(1);
        }
        regainHealthCoroutine = null;
    }

    public void CollectHealth()
    {
        health = Mathf.Min(health + 5, 100f); // Increase health but not above 100
        UpdateHealthUI(); // Update the health UI

        if (regainHealthCoroutine != null)
        {
            StopCoroutine(regainHealthCoroutine);
            regainHealthCoroutine = null;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = Mathf.RoundToInt(health).ToString(); // Convert health to integer

            // Change color based on health value
            if (health > 80)
            {
                healthText.color = Color.green;
            }
            else if (health > 50)
            {
                healthText.color = Color.yellow;
            }
            else if (health > 30)
            {
                healthText.color = new Color(1, 0.5f, 0); // Orange color
            }
            else if (health > 10)
            {
                healthText.color = Color.red;
            }
            else
            {
                healthText.color = new Color(0.5f, 0, 0); // Dark red color
            }
        }
    }
}

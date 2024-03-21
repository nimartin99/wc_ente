using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class WaveManager : MonoBehaviour
{
    public GameObject waterPrefab;
    private int cycleCount = 0;

    private void Start()
    {
        StartCoroutine(RepeatWaveCycle());
    }

    IEnumerator RepeatWaveCycle()
    {
        while (cycleCount < 10) // Adjust the desired number of cycles
        {
            yield return new WaitForSeconds(30f); // Initial delay before each cycle

            Debug.Log("Wave in Coming");
            GameObject Alert = GameObject.Find("Alert");
            Alert.GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(5f); // Warning 5 seconds before filling water
            Alert.GetComponent<Image>().enabled = false;
            yield return StartCoroutine(MoveWaterOverTime(2f, 0.2f)); // Adjust the target Z position and duration as needed
            yield return new WaitForSeconds(3f); // Delay after filling water
            yield return StartCoroutine(MoveWaterOverTime(2f, 0.4f)); // Adjust the target Z position and duration as needed
            GameInitializer.Instance.WaveSet.SetActive(false);
            GameInitializer.Instance.WaveSet2.SetActive(true);
            yield return new WaitForSeconds(3f); // Delay after filling water
            GameInitializer.Instance.WaveSet2.SetActive(false);

            cycleCount++;
        }
    }

    IEnumerator MoveWaterOverTime(float duration, float targetZPosition)
    {
        Vector3 startPosition = waterPrefab.transform.localPosition; // Use localPosition
        float elapsedTime = 0f;
      //  DuckControls.instance.Waves_Set.SetActive(true);
    ///   //// GameInitializer.Instance.playerPrefab = Instantiate(GameInitializer.Instance.Waves_Set.transform, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        print("Tunnel Fill");
        while (elapsedTime < duration)
        {
            // Explicitly set only Z position in modifiedTargetPosition
            Vector3 modifiedTargetPosition = new Vector3(startPosition.x, startPosition.y, targetZPosition);
            GameInitializer.Instance.WaveSet.SetActive(true);
          //  waterPrefab.transform.localPosition = Vector3.Lerp(startPosition, modifiedTargetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}

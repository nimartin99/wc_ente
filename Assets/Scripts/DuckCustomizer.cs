using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCustomizer : MonoBehaviour
{
    public GameObject hat;
    public int hatIndex;
    public Camera camera;
    public RenderTexture renderTexture;
    public int size = 256;
    public static bool randomStartHat = true;

    public GameObject[] possibleHats;

    public int hatCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (randomStartHat) {
            ChangeHat(Random.Range(0, possibleHats.Length));
        }
        renderTexture = new RenderTexture(size, size, 16);
        camera.targetTexture = renderTexture;
    }

    public void SetHat(int index) {
        Destroy(hat);
        hat = Instantiate(possibleHats[index],transform.position,transform.rotation,this.transform);
        hatCounter = index;
    }

    public void ChangeHat(int hatIncrease)
    {
        Destroy(hat);
        hatCounter += hatIncrease;
        if (hatCounter >= possibleHats.Length)
        {
            hatCounter = 0;
        } else if (hatCounter < 0) {
            hatCounter = possibleHats.Length - 1;
        }
        hat = Instantiate(possibleHats[hatCounter],transform.position,transform.rotation,this.transform);
        hatIndex = hatCounter;
    }


}

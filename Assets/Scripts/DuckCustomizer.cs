using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCustomizer : MonoBehaviour
{
    public GameObject hat;
    public Camera camera;
    public RenderTexture renderTexture;
    public int size = 256;


    public GameObject[] possibleHats;

    public int hatCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        ChangeHat(Random.Range(0, possibleHats.Length));
        renderTexture = new RenderTexture(size, size, 16);
        camera.targetTexture = renderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
    }


}

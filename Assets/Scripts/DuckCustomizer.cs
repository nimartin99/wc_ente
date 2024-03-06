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

    private int hatCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(hat);
        hat = Instantiate(possibleHats[hatCounter],transform.position,transform.rotation,this.transform);
        renderTexture = new RenderTexture(size, size, 16);
        camera.targetTexture = renderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeHat()
    {
        Destroy(hat);
        hat = Instantiate(possibleHats[hatCounter],transform.position,transform.rotation,this.transform);
        hatCounter++;
        if (hatCounter >= possibleHats.Length)
        {
            hatCounter = 0;
        }
    }


}
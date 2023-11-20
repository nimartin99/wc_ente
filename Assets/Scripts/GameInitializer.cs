using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance { get; private set; }
    [SerializeField] private Transform playerPrefab;
    
    //set standard keycodes for first two players
    private KeyCode[,] standardCodes =
    {
        { KeyCode.W,KeyCode.A,KeyCode.S,KeyCode.D},
        { KeyCode.UpArrow,KeyCode.LeftArrow,KeyCode.DownArrow,KeyCode.RightArrow}
    };
    
    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
    }

    public void StartGame(UIControl uiControl) {
        for (int i = 0; i < uiControl.players.Count; i++) {
            Transform playerAnchor = Instantiate(playerPrefab);
            playerAnchor.position = new Vector3(0, 0, 4f);
            playerAnchor.eulerAngles = new Vector3(0, 0, 90 * i);
            DuckControls playerScript = playerPrefab.GetComponent<DuckControls>();
            playerScript.keyUp = uiControl.players[i].playerUp != KeyCode.None ?
                uiControl.players[i].playerUp: standardCodes[i,0];
            playerScript.keyLeft = uiControl.players[i].playerLeft != KeyCode.None ? uiControl.players[i].playerLeft: standardCodes[i,1];;
            playerScript.keyDuck = uiControl.players[i].playerDuck != KeyCode.None ? uiControl.players[i].playerDuck: standardCodes[i,2];;
            playerScript.keyRight = uiControl.players[i].playerRight != KeyCode.None ? uiControl.players[i].playerRight: standardCodes[i,3];;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance { get; private set; }
    [SerializeField] private Transform playerPrefab;
    [SerializeField] private Transform powerUpPrefab;
    
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
            DuckControls playerScript = playerAnchor.GetChild(0).GetComponent<DuckControls>();
            playerScript.keyUp = uiControl.players[i].playerUp;
            playerScript.keyLeft = uiControl.players[i].playerLeft;
            playerScript.keyDuck = uiControl.players[i].playerDuck;
            playerScript.keyRight = uiControl.players[i].playerRight;
        }
        Instantiate(powerUpPrefab, new Vector3(0, -0.4f, 0), Quaternion.identity);
    }
}

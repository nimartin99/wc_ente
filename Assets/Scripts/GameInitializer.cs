using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance { get; private set; }
    [SerializeField] private Transform playerPrefab;
    [SerializeField] private Transform powerUpPrefab;
    [SerializeField] private Transform pipePrefab;
    
    // Standard keycodes for first two players
    private KeyCode[,] standardCodes =
    {
        { KeyCode.W,KeyCode.A,KeyCode.S,KeyCode.D},
        { KeyCode.UpArrow,KeyCode.LeftArrow,KeyCode.DownArrow,KeyCode.RightArrow}
    };
    
    private void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
    }

    /// <summary>
    /// The Method that starts the game by initializing and spawning all the game objects in the scene
    /// </summary>
    /// <param name="uiControl">Instance of the uiControl script to get information with which parameters to start the
    /// game</param>
    public void StartGame(UIControl uiControl) {
        SpawnLevelPrefabs();
        SpawnPlayers(uiControl);
        ObstacleSpawner.Instance.spawnObstacles = true;
        // Instantiate a test powerup
        Instantiate(powerUpPrefab, new Vector3(0, -0.4f, 0), Quaternion.identity);
    }

    private void SpawnLevelPrefabs() {
        Instantiate(pipePrefab, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
    }

    private void SpawnPlayers(UIControl uiControl) {
        for (int i = 0; i < uiControl.players.Count; i++) {
            // Spawn a player prefab
            Transform playerAnchor = Instantiate(playerPrefab);
            playerAnchor.position = new Vector3(0, 0, 4f);
            playerAnchor.eulerAngles = new Vector3(0, 0, 90 * i);
            DuckControls playerScript = playerAnchor.GetChild(0).GetComponent<DuckControls>();
            
            // Set keycodes for players
            playerScript.keyUp = uiControl.players[i].playerUp != KeyCode.None ? uiControl.players[i].playerUp: standardCodes[i,0];
            playerScript.keyLeft = uiControl.players[i].playerLeft != KeyCode.None ? uiControl.players[i].playerLeft: standardCodes[i,1];
            playerScript.keyDuck = uiControl.players[i].playerDuck != KeyCode.None ? uiControl.players[i].playerDuck: standardCodes[i,2];
            playerScript.keyRight = uiControl.players[i].playerRight != KeyCode.None ? uiControl.players[i].playerRight: standardCodes[i,3];
        }
    }
}

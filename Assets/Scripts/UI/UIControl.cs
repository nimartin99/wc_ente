using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControl : MonoBehaviour
{
    public static UIControl Instance { get; private set; }
    
    private UIDocument _uiDocument;
    
    // Key config UI
    public List<PlayerInfo> players = new List<PlayerInfo>();

    // Represents the player for which keys are being captured (-1 = none)
    private int playerCapturing = -1;
    // the key number that is configured (0 = up, 1 = left, 2 = down, 3 = right)
    private int currentCaptureKey;

    private Button _playButton;
    
    private void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        }
    }
    
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        VisualElement root = _uiDocument.rootVisualElement;
        
        // Configure the button that starts the game
        _playButton = root.Q<Button>("playButton");
        _playButton.RegisterCallback<ClickEvent>(StartGame);
        
        // For testing purposes we always start with 2 players
        PlayerInfo player1Info = new PlayerInfo();
        PlayerInfo player2Info = new PlayerInfo();
        players.Add(player1Info);
        players.Add(player2Info);

        // Configure the two players by searching the belonging button and labels in the UI root
        for (int i = 0; i < players.Count; i++) {
            players[i].activateButton = root.Q<Button>("player" + (i + 1) + "ActivateButton");
            
            players[i].playerUpLabel = root.Q<Label>("player" + (i + 1) + "UpLabel");
            players[i].playerLeftLabel = root.Q<Label>("player" + (i + 1) + "LeftLabel");
            players[i].playerDuckLabel = root.Q<Label>("player" + (i + 1) + "DuckLabel");
            players[i].playerRightLabel = root.Q<Label>("player" + (i + 1) + "RightLabel");

            players[i].activateButton.RegisterCallback<ClickEvent>(ActivateCapturing);
        }
    }
    
    /// <summary>
    /// Activates the capturing for the key configuration for a player
    /// </summary>
    /// <param name="clickEvent"></param>
    private void ActivateCapturing(ClickEvent clickEvent) {
        VisualElement target = (Button) clickEvent.target;
        // Get which player the keys are configured for by slicing the number from the button name
        playerCapturing = target.name[6] - '0';
    }

    /// <summary>
    /// Method to start the game
    /// </summary>
    /// <param name="clickEvent">The click that executes the start of the game</param>
    private void StartGame(ClickEvent clickEvent) {
        _uiDocument.enabled = false;
        GameInitializer.Instance.StartGame(this);
    }

    private void Update() {
        // If we are capturing key for a player (playerCapturing != -1) and a key is pressed start capturing
        if (playerCapturing != -1 && currentCaptureKey <= 3 && Input.anyKeyDown) {
            // Cycle through all keys and find which key is pressed
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKey(keyCode)) {
                    PlayerInfo player = players[playerCapturing - 1];
                    // Set the key to the belonging direction by switching over the currentCaptureKey
                    switch (currentCaptureKey) {
                        case 0:
                            player.playerUp = keyCode;
                            player.playerUpLabel.text = keyCode.ToString();
                            currentCaptureKey++;
                            break;
                        case 1:
                            player.playerLeft = keyCode;
                            player.playerLeftLabel.text = keyCode.ToString();
                            currentCaptureKey++;
                            break;
                        case 2:
                            player.playerDuck = keyCode;
                            player.playerDuckLabel.text = keyCode.ToString();
                            currentCaptureKey++;
                            break;
                        case 3:
                            player.playerRight = keyCode;
                            player.playerRightLabel.text = keyCode.ToString();
                            // Reset the playerCapturing and currentCaptureKey
                            playerCapturing = -1;
                            currentCaptureKey = 0;
                            break;
                    }
                }
            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControl : MonoBehaviour
{
    public static UIControl Instance { get; private set; }
    
    private UIDocument _uiDocument;
    
    // Key config UI
    private List<PlayerInfo> _players = new List<PlayerInfo>();

    private int playerCapturing = -1;
    private int currentCaptureKey = 0;
    
    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        VisualElement root = _uiDocument.rootVisualElement;
        
        // For testing purposes we always start with 2 players
        PlayerInfo player1Info = new PlayerInfo(
            // root.Q<Button>("_player1ActivateButton"),
            // root.Q<Label>("_player1UpLabel"),
            // root.Q<Label>("_player1LeftLabel"),
            // root.Q<Label>("_player1DuckLabel"),
            // root.Q<Label>("_player1RightLabel")
        );
        PlayerInfo player2Info = new PlayerInfo(
            // root.Q<Button>("_player2ActivateButton"),
            // root.Q<Label>("_player2UpLabel"),
            // root.Q<Label>("_player2LeftLabel"),
            // root.Q<Label>("_player2DuckLabel"),
            // root.Q<Label>("_player2RightLabel")
        );
        _players.Add(player1Info);
        _players.Add(player2Info);

        for (int i = 0; i < _players.Count; i++) {
            _players[i].activateButton = root.Q<Button>("player" + (i + 1) + "ActivateButton");
            
            _players[i].playerUpLabel = root.Q<Label>("player" + (i + 1) + "UpLabel");
            _players[i].playerLeftLabel = root.Q<Label>("player" + (i + 1) + "LeftLabel");
            _players[i].playerDuckLabel = root.Q<Label>("player" + (i + 1) + "DuckLabel");
            _players[i].playerRightLabel = root.Q<Label>("player" + (i + 1) + "RightLabel");

            _players[i].activateButton.RegisterCallback<ClickEvent>(ActivateCapturing);
        }
    }
    
    private void ActivateCapturing(ClickEvent clickEvent) {
        VisualElement target = (Button) clickEvent.target;
        
    }

    private void Update() {
        if ((playerCapturing != -1 && currentCaptureKey <= 3) && Input.anyKeyDown) {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKey(keyCode)) {
                    PlayerInfo player = _players[playerCapturing];
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
                            playerCapturing = -1;
                            currentCaptureKey = -1;
                            break;
                    }
                }
            }
        }
    }
}


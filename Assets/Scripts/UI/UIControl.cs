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

    private int playerCapturing = -1;
    private int currentCaptureKey;

    private Button _playButton;
    
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
        _playButton = root.Q<Button>("playButton");
        _playButton.RegisterCallback<ClickEvent>(StartGame);
        
        // For testing purposes we always start with 2 players
        PlayerInfo player1Info = new PlayerInfo();
        PlayerInfo player2Info = new PlayerInfo();
        players.Add(player1Info);
        players.Add(player2Info);

        for (int i = 0; i < players.Count; i++) {
            players[i].activateButton = root.Q<Button>("player" + (i + 1) + "ActivateButton");
            
            players[i].playerUpLabel = root.Q<Label>("player" + (i + 1) + "UpLabel");
            players[i].playerLeftLabel = root.Q<Label>("player" + (i + 1) + "LeftLabel");
            players[i].playerDuckLabel = root.Q<Label>("player" + (i + 1) + "DuckLabel");
            players[i].playerRightLabel = root.Q<Label>("player" + (i + 1) + "RightLabel");

            players[i].activateButton.RegisterCallback<ClickEvent>(ActivateCapturing);
        }
    }
    
    private void ActivateCapturing(ClickEvent clickEvent) {
        VisualElement target = (Button) clickEvent.target;
        playerCapturing = target.name[6] - '0';
    }

    private void StartGame(ClickEvent clickEvent) {
        _uiDocument.enabled = false;
        GameInitializer.Instance.StartGame(this);
    }

    private void Update() {
        if (playerCapturing != -1 && currentCaptureKey <= 3 && Input.anyKeyDown) {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKey(keyCode)) {
                    PlayerInfo player = players[playerCapturing - 1];
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
                            currentCaptureKey = 0;
                            break;
                    }
                }
            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControl : MonoBehaviour
{
    public static UIControl Instance { get; private set; }
     VisualElement root;
    private UIDocument _uiDocument;
   [SerializeField] private VisualTreeAsset _keyConfiguration;
   [SerializeField] private VisualTreeAsset _gameOverUI;
    
    // Key config UI
    public List<PlayerInfo> players = new List<PlayerInfo>();

    private int playerCapturing = -1;
    private int currentCaptureKey;

    private Button _playButton;
    private Button _BegingButton;
    
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

         root = _uiDocument.rootVisualElement;

          // Configure the button that starts the menu
        _BegingButton = root.Q<Button>("BegingButton");
        _BegingButton.RegisterCallback<ClickEvent>(StartMenu);   
    }
    
    public void EndGame() {
        Debug.Log("Game is over");
        _uiDocument.enabled = true;
        _uiDocument.visualTreeAsset = _gameOverUI;
    }
    
    /// <summary>
    /// Activates the capturing for the key configuration for a player
    /// </summary>
    /// <param name="clickEvent"></param>

    private void ActivateCapturing(ClickEvent clickEvent) {
        VisualElement target = (Button) clickEvent.target;
        playerCapturing = target.name[6] - '0';
    }

    private void StartGame(ClickEvent clickEvent) {
        _uiDocument.enabled = false;
        GameInitializer.Instance.StartGame(this);
    }

        private void StartMenu(ClickEvent clickEvent) {
        _uiDocument.visualTreeAsset = _keyConfiguration;
        ConfigurationScreen();
    }

    private void ConfigurationScreen() {
 // Configure the button that starts the game
         root = _uiDocument.rootVisualElement;
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


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
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

    private VisualElement _playerNameContainer;
    private Button _addPlayerButton;
    private Button _playButton;
    private Button _BegingButton;
    public DuckCustomizer[] customizers;
    
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

         // Configure the button and container that lets you add new players
         _playerNameContainer = root.Q<VisualElement>("PlayerNameContainer");
         _addPlayerButton = root.Q<Button>("AddPlayerButton");
         _addPlayerButton.RegisterCallback<ClickEvent>(AddPlayer);  
         
         // Configure the button that starts the menu
        _BegingButton = root.Q<Button>("BegingButton");
        _BegingButton.RegisterCallback<ClickEvent>(StartMenu);   
    }
    
    public void EndGame() {
        Debug.Log("Game is over");
        _uiDocument.enabled = true;
        _uiDocument.visualTreeAsset = _gameOverUI;
    }

    private void AddPlayer(ClickEvent clickEvent) {
        TextField playerTextField = new TextField();
        playerTextField.name = "UserName" + (_playerNameContainer.childCount + 1);
        playerTextField.AddToClassList("playerNameField");
        playerTextField.label = "Player" + (_playerNameContainer.childCount + 1) + ":";
        _playerNameContainer.Add(playerTextField);
        if (_playerNameContainer.childCount == 4) {
            _addPlayerButton.visible = false;
        }
    }
    
    /// <summary>
    /// Activates the capturing for the key configuration for a player
    /// </summary>
    /// <param name="clickEvent"></param>
    private void ActivateCapturing(ClickEvent clickEvent) {
        VisualElement target = (Button) clickEvent.target;
        playerCapturing = target.name[6] - '0';
    }

    private void ChangeHat(ClickEvent clickEvent)
    {
        VisualElement target = (Button) clickEvent.target;
        int playerId = target.name[6]- '0';
        playerId -= 1;
        customizers[playerId].changeHat();
        RenderTexture renderTexture = players[playerId].customizer.renderTexture;
        Texture2D cameraTexture = new Texture2D(renderTexture.width,renderTexture.height,DefaultFormat.LDR,1,TextureCreationFlags.None);
        Graphics.CopyTexture(renderTexture, cameraTexture);
        players[playerId].duckDisplay.style.backgroundImage = cameraTexture;
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
        for (int i = 0; i < _playerNameContainer.childCount; i++) {
            PlayerInfo playerInfo = new PlayerInfo();
            TextField playerNameTextfield = _playerNameContainer.Q<TextField>("UserName" + (i + 1));
            playerInfo.playerName = playerNameTextfield.value;
            players.Add(playerInfo);
            Debug.Log(playerInfo.playerName = playerNameTextfield.value);
        }

        for (int i = 0; i < 4; i++) {
            VisualElement playerKeyRow = root.Q<VisualElement>("player" + (i + 1) + "KeyRow");
            playerKeyRow.style.display = DisplayStyle.None;
        }

        // Configure the players by searching the belonging button and labels in the UI root
        for (int i = 0; i < players.Count; i++) {
            VisualElement playerKeyRow = root.Q<VisualElement>("player" + (i + 1) + "KeyRow");
            playerKeyRow.style.display = DisplayStyle.Flex;
            Button activatorButton = root.Q<Button>("player" + (i + 1) + "ActivateButton");
            activatorButton.text = players[i].playerName;
            players[i].activateButton = activatorButton;
            
            players[i].playerUpLabel = root.Q<Label>("player" + (i + 1) + "UpLabel");
            players[i].playerLeftLabel = root.Q<Label>("player" + (i + 1) + "LeftLabel");
            players[i].playerDuckLabel = root.Q<Label>("player" + (i + 1) + "DuckLabel");
            players[i].playerRightLabel = root.Q<Label>("player" + (i + 1) + "RightLabel");
            
            //duck Visualization

            players[i].customizer = customizers[i];
            players[i].duckDisplay = root.Q<VisualElement>("player" + (i + 1) + "DuckDisplay");
            players[i].changeHatButton = root.Q<Button>("player" + (i + 1) + "ChangeHat");
            RenderTexture renderTexture = players[i].customizer.renderTexture;
            Texture2D cameraTexture = new Texture2D(renderTexture.width,renderTexture.height,DefaultFormat.LDR,1,TextureCreationFlags.None);
            Graphics.CopyTexture(renderTexture, cameraTexture);
            players[i].duckDisplay.style.backgroundImage = cameraTexture;
            players[i].changeHatButton.RegisterCallback<ClickEvent>(ChangeHat);
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
        else
        {
            for (int i = 0; i < players.Count; i++) {
            
                //duck Visualization
                players[i].duckDisplay = root.Q<VisualElement>("player" + (i + 1) + "DuckDisplay");
                RenderTexture renderTexture = players[i].customizer.renderTexture;
                Texture2D cameraTexture = new Texture2D(renderTexture.width,renderTexture.height,DefaultFormat.LDR,1,TextureCreationFlags.None);
                Graphics.CopyTexture(renderTexture, cameraTexture);
                players[i].duckDisplay.style.backgroundImage = cameraTexture;
            }
        }
       
    }
    
    
}


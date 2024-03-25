using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class UIControl : MonoBehaviour
{ 
    public static UIControl Instance { get; private set; }
    VisualElement root;
    private UIDocument _uiDocument;
   [SerializeField] private VisualTreeAsset _keyConfiguration;
   [SerializeField] private VisualTreeAsset _gameOverUI;

   [SerializeField] private ObstacleSpawner _obstacleSpawner;
   [SerializeField] private ObstacleSpawner _powerUpSpawner;
   
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
         
         // Fill the names with random funny ones
         for (int i = 0; i < _playerNameContainer.childCount; i++) {
             TextField playerNameTextfield = _playerNameContainer.Q<TextField>("UserName" + (i + 1));
             playerNameTextfield.value = PlayerInfo.duckNames[Random.Range(0, PlayerInfo.duckNames.Count)];
         }
         
         // Configure the button that starts the menu
        _BegingButton = root.Q<Button>("BegingButton");
        _BegingButton.RegisterCallback<ClickEvent>(StartMenu);   
    }
    
    public void EndGame(PlayerInfo winningPlayer) {
        Debug.Log("Game is over");
        _uiDocument.enabled = true;
        _uiDocument.visualTreeAsset = _gameOverUI;
        root = _uiDocument.rootVisualElement;
        Label winningPlayerLabel = root.Q<Label>("winningPlayerLabel");
        winningPlayerLabel.text = "GAME OVER!\n" + winningPlayer.playerName + " won!";
    }

    private void AddPlayer(ClickEvent clickEvent) {
        TextField playerTextField = new TextField();
        playerTextField.name = "UserName" + (_playerNameContainer.childCount + 1);
        playerTextField.AddToClassList("playerNameField");
        playerTextField.label = "Player" + (_playerNameContainer.childCount + 1) + ":";
        playerTextField.value = PlayerInfo.duckNames[Random.Range(0, PlayerInfo.duckNames.Count)];
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
        int hatIncrease = target.name[16] == 'R' ? 1 : -1;
        customizers[playerId].ChangeHat(hatIncrease);
        RenderTexture renderTexture = players[playerId].customizer.renderTexture;
        Texture2D cameraTexture = new Texture2D(renderTexture.width,renderTexture.height,DefaultFormat.LDR,1,TextureCreationFlags.None);
        Graphics.CopyTexture(renderTexture, cameraTexture);
        players[playerId].duckDisplay.style.backgroundImage = cameraTexture;
    }

    private void StartGame(ClickEvent clickEvent) {
        // Slider stuff
        SliderInt difficultySlider = root.Q<SliderInt>("difficultySlider");
        _obstacleSpawner.spawnDelayAvg = difficultySlider.value;
        
        _uiDocument.enabled = false;
        GameInitializer.Instance.StartIntro(Mathf.Clamp(difficultySlider.value, 5, 10));
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
        
        for (int i = 0; i < _playerNameContainer.childCount; i++) {
            PlayerInfo playerInfo = new PlayerInfo();
            TextField playerNameTextfield = _playerNameContainer.Q<TextField>("UserName" + (i + 1));
            if (playerNameTextfield.value == "") {
                playerInfo.playerName = PlayerInfo.duckNames[Random.Range(0, PlayerInfo.duckNames.Count)];
            } else {
                playerInfo.playerName = playerNameTextfield.value;
            }
            players.Add(playerInfo);
            Debug.Log(playerInfo.playerName = playerNameTextfield.value);
        }

        for (int i = 0; i < 4; i++) {
            VisualElement playerKeyRow = root.Q<VisualElement>("player" + (i + 1) + "KeyRow");
            playerKeyRow.visible = false;
            customizers[i].gameObject.SetActive(false);
        }

        // Configure the players by searching the belonging button and labels in the UI root
        for (int i = 0; i < players.Count; i++) {
            VisualElement playerKeyRow = root.Q<VisualElement>("player" + (i + 1) + "KeyRow");
            playerKeyRow.visible = true;
            Button activatorButton = root.Q<Button>("player" + (i + 1) + "ActivateButton");
            activatorButton.text = players[i].playerName;
            players[i].activateButton = activatorButton;
            
            players[i].playerUpLabel = root.Q<Label>("player" + (i + 1) + "UpLabel");
            players[i].playerLeftLabel = root.Q<Label>("player" + (i + 1) + "LeftLabel");
            players[i].playerDuckLabel = root.Q<Label>("player" + (i + 1) + "DuckLabel");
            players[i].playerRightLabel = root.Q<Label>("player" + (i + 1) + "RightLabel");
            
            //duck Visualization
            customizers[i].gameObject.SetActive(true);
            players[i].customizer = customizers[i];
            players[i].duckDisplay = root.Q<VisualElement>("player" + (i + 1) + "DuckDisplay");
            players[i].changeHatButtonLeft = root.Q<Button>("player" + (i + 1) + "ChangeHatLeft");
            players[i].changeHatButtonRight = root.Q<Button>("player" + (i + 1) + "ChangeHatRight");
            RenderTexture renderTexture = players[i].customizer.renderTexture;
            Texture2D cameraTexture = new Texture2D(renderTexture.width,renderTexture.height,DefaultFormat.LDR,1,TextureCreationFlags.None);
            Graphics.CopyTexture(renderTexture, cameraTexture);
            players[i].duckDisplay.style.backgroundImage = cameraTexture;
            players[i].changeHatButtonLeft.RegisterCallback<ClickEvent>(ChangeHat);
            players[i].changeHatButtonRight.RegisterCallback<ClickEvent>(ChangeHat);
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
            // Removed because we now use the configure camera
            // for (int i = 0; i < players.Count; i++) {
            //
            //     //duck Visualization
            //     players[i].duckDisplay = root.Q<VisualElement>("player" + (i + 1) + "DuckDisplay");
            //     RenderTexture renderTexture = players[i].customizer.renderTexture;
            //     Texture2D cameraTexture = new Texture2D(renderTexture.width,renderTexture.height,DefaultFormat.LDR,1,TextureCreationFlags.None);
            //     Graphics.CopyTexture(renderTexture, cameraTexture);
            //     players[i].duckDisplay.style.backgroundImage = cameraTexture;
            // }
        }
       
    }
    
    
}


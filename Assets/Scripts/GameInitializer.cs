using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
    
public class GameInitializer : MonoBehaviour {
    public bool gameRunning;
    public List<Transform> currentPlayers = new List<Transform>();
    public List<APlayer> activePlayers = new List<APlayer>();
    public static GameInitializer Instance { get; private set; }
    [SerializeField] private Transform playerPrefab;
    [SerializeField] private Transform powerUpSpawner;
    [SerializeField] private Transform pipePrefab;
    [SerializeField] private Transform playerAnchorPrefab;
    public DuckCustomizer[] allPlayercustomizer;
    private Transform pipeSpawner;
    public GameObject WaveSet;
    public GameObject WaveSet2;
    private Transform _zoomInCamera;
    private float powerUpSpawnDelay;
    
    // Intro
    [SerializeField] private PlayableDirector director;

    private Color[] possibleColors =
    {
        Color.yellow,
        Color.cyan, 
        Color.magenta,
        Color.blue, 
        Color.green, 
        Color.red, 
        Color.white, 
        Color.gray, 
    };
    
    // Standard keycodes for first two players
    private KeyCode[,] standardCodes =
    {
        { KeyCode.W,KeyCode.A,KeyCode.S,KeyCode.D },
        { KeyCode.UpArrow,KeyCode.LeftArrow,KeyCode.DownArrow,KeyCode.RightArrow },
        { KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L },
        { KeyCode.H,KeyCode.V,KeyCode.B,KeyCode.N },
    };
    
    private void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        }  else { 
            Instance = this; 
        }
    }

    private void Update() {
        // End the game
        if (currentPlayers.Count == 1 && gameRunning) {
            gameRunning = false;
            UIControl.Instance.EndGame(currentPlayers[0].GetComponent<DuckControls>().playerInfo);
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
        powerUpSpawner.gameObject.SetActive(true);
        powerUpSpawner.GetComponent<PowerUpSpawner>().spawnDelayAvg = powerUpSpawnDelay;
        ObstacleSpawner.Instance.spawnObstacles = true;
        Light.Instance.spawnLight = true;
        gameRunning = true;
    }

    public void StartIntro(float powerUpSpawnDelaySlider) {
        powerUpSpawnDelay = powerUpSpawnDelaySlider;
        director.Play();
        StartCoroutine(AudioManager.Instance.FadeOut(AudioManager.Instance.audioSources[0], 6f));
    }

    public void EndIntro() {
        Debug.Log("EndIntro");
        director.Stop();
        StartGame(UIControl.Instance);
    }
    
    private void SpawnLevelPrefabs() {
        pipeSpawner = Instantiate(pipePrefab, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
    }

    private IEnumerator  DeactivateZoomInCamera(float delay) {
        yield return new WaitForSeconds(delay);
        _zoomInCamera.gameObject.SetActive(false);
    }

    private void SpawnPlayers(UIControl uiControl) {
        Transform playerAnchorParent = Instantiate(playerAnchorPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));

        for (int i = 0; i < uiControl.players.Count; i++) {
            // Spawn a player prefab
            Transform playerAnchor = Instantiate(playerPrefab, playerAnchorParent);
            playerAnchor.position = new Vector3(0, 0, 0);
            playerAnchor.eulerAngles = new Vector3(0, 0, 90 * i);

            if (i == 0) {
                _zoomInCamera = playerAnchor.GetChild(2);
                _zoomInCamera.gameObject.SetActive(true);
            }

            DuckControls playerScript = playerAnchor.GetComponent<DuckControls>();
            playerScript.SetColor(possibleColors[i]);
            playerScript.SetHat(uiControl.players[i].customizer.hatCounter);
            playerScript.playerInfo = uiControl.players[i];
            currentPlayers.Add(playerAnchor);


            APlayer playerReference = new APlayer();
            playerReference.isActive = true;
            playerReference.myIndex = i;
            playerReference.player = playerAnchor;

            GameInitializer.Instance.activePlayers.Add(playerReference);

            // Set keycodes for players
            playerScript.keyUp = uiControl.players[i].playerUp != KeyCode.None ? uiControl.players[i].playerUp: standardCodes[i,0];
            playerScript.keyLeft = uiControl.players[i].playerLeft != KeyCode.None ? uiControl.players[i].playerLeft: standardCodes[i,1];
            playerScript.keyDuck = uiControl.players[i].playerDuck != KeyCode.None ? uiControl.players[i].playerDuck: standardCodes[i,2];
            playerScript.keyRight = uiControl.players[i].playerRight != KeyCode.None ? uiControl.players[i].playerRight: standardCodes[i,3];
        }
        
        // Deactivate the zoomInCamera after the amount of time
        StartCoroutine(DeactivateZoomInCamera(0.05f));
        
        // Set the Camera as child of the playerAnchorParent
        // Camera.main.transform.SetParent(playerAnchorParent);
        // Camera.main.transform.position = new Vector3(playerAnchorParent.position.x, playerAnchorParent.position.y,
        //     playerAnchorParent.position.z + 1f);
        
        PipeGenerator pipeGenerator = pipeSpawner.GetComponent<PipeGenerator>();
        pipeGenerator.objectToMove = playerAnchorParent.gameObject;
    }
}

[System.Serializable]
public class APlayer
{
    
    public bool isActive;
    public Transform player;
    public int myIndex;
}

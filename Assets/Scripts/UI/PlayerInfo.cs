using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
public class PlayerInfo {
    public String playerName;
    public Button activateButton;
    public Label playerUpLabel;
    public Label playerLeftLabel;
    public Label playerDuckLabel;
    public Label playerRightLabel;
    public VisualElement duckDisplay;
    public Button changeHatButton;
    public DuckCustomizer customizer;
    
    public KeyCode playerUp;
    public KeyCode playerLeft;
    public KeyCode playerDuck;
    public KeyCode playerRight;
    
    public PlayerInfo() {}
    
    public PlayerInfo(Button activateButton, Label playerUpLabel, Label playerLeftLabel, Label playerDuckLabel, Label playerRightLabel) {
        this.activateButton = activateButton;
        this.playerUpLabel = playerUpLabel;
        this.playerLeftLabel = playerLeftLabel;
        this.playerDuckLabel = playerDuckLabel;
        this.playerRightLabel = playerRightLabel;
    }
}

using UnityEngine;
using UnityEngine.UIElements;
public class PlayerInfo {
    // Button to activate the key configuration for this player
    public Button activateButton;
    
    // Labels that shows which key is configured for the control
    public Label playerUpLabel;
    public Label playerLeftLabel;
    public Label playerDuckLabel;
    public Label playerRightLabel;
    
    // The keycodes that actually are used later to control the player
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

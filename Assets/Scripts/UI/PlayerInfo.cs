using UnityEngine;
using UnityEngine.UIElements;
public class PlayerInfo {
    public Button activateButton;
    public Label playerUpLabel;
    public Label playerLeftLabel;
    public Label playerDuckLabel;
    public Label playerRightLabel;
    
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

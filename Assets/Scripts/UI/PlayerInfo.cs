using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


[System.Serializable]
public class PlayerInfo {
    public String playerName;
    public Button activateButton;
    public Label playerUpLabel;
    public Label playerLeftLabel;
    public Label playerDuckLabel;
    public Label playerRightLabel;
    public VisualElement duckDisplay;
    public Button changeHatButtonLeft;
    public Button changeHatButtonRight;
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
    
    public static List<string> duckNames = new List<string>()
    {
        "Quackmire", "Duck Norris", "Feather Locklear", "Bill Nye the Science Guy", "Quack Sparrow",
        "James Pond", "Duck Vader", "Quackula", "Moby Duck", "Pond Solo", "Sir Quacks-a-Lot",
        "Daffy the Drafty", "Drake Quackson", "Quackie Chan", "Flapper", "Sir Quackalot",
        "Waddles", "Puddle", "Squirts", "Bobbin", "Splash", "Bubbles", "Ducky Jr.", "Quackpot",
        "Paddles", "Quackerjack", "Ducktape", "Quackenstein", "Duckbeak", "Quack Attack",
        "Duckaroo", "Quackzilla", "Duckling Dynamo", "Quacko", "Ducktastic", "Quackalicious",
        "Duckaroo Banzai", "Quack Commander", "Duck Dodger", "Quackshot", "Duck-a-l'Orange",
        "Quacksmith", "Ducknado", "Quackaroo", "Duckweed", "Quackberry", "Duckface",
        "Quackmamba", "Ducktail", "Quacknificent", "Duckworth", "Quackaroo Jack", "Duckula",
        "Quacknado", "Duckminster Fuller", "Quackaroo Jones", "Duckbot", "Quackenstein's Monster",
        "Ducktopus", "Quackula's Bride", "Duckatron", "Quackolyte", "Duckenstein", "Quackling",
        "Duckzilla", "Quackaroo Kid", "Duckminster", "Quacken", "Duckaroo Duke", "Quackaroo",
        "Duckmeister", "Quackaroo Prince", "Duckmander", "Quackaroo King", "Duckmo", "Quackaroo Knight",
        "Duckmax", "Quackaroo Lord", "Duckminator", "Quackaroo Baron", "Duckmosis", "Quackaroo Earl",
        "Duckmire", "Quackaroo Count", "Duckmon", "Quackaroo Viscount", "Duckmore", "Quackaroo Marquis",
        "Duckmos", "Quackaroo Duke", "Duckmont", "Quackaroo Prince", "Duckmoor", "Quackaroo King",
        "Duckmoon", "Quackaroo Knight", "Duckmoss", "Quackaroo Lord", "Duckmoth", "Quackaroo Baron",
        "Duckmount", "Quackaroo Earl", "Duckmound", "Quackaroo Count", "Duckmould", "Quackaroo Viscount",
        "Duckmow", "Quackaroo Marquis", "Duckmower", "Quackaroo Duke", "Duckmown", "Quackaroo Prince",
        "Duckmox", "Quackaroo King", "Duckmud", "Quackaroo Knight", "Duckmug", "Quackaroo Lord"
    };
}

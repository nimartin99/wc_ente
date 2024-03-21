using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public GameObject[] allPlayersInfoHolder;
    public RawImage[] allPlayersInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameInitializer.Instance.currentPlayers.Count > 1)
        {
            DisplayHEalthandAvatars();
        }
        
    }

    void DisplayHEalthandAvatars()
    {
        for (int i = 0; i < allPlayersInfoHolder.Length; i++)
        {
            if (i < GameInitializer.Instance.activePlayers.Count)
            {
                if (GameInitializer.Instance.activePlayers[i].isActive)
                {
                    allPlayersInfoHolder[i].SetActive(true);
                }
                else
                {
                    allPlayersInfoHolder[i].SetActive(false);
                }
                
            }
            else
            {
                allPlayersInfoHolder[i].SetActive(false);
            }

        }
        SetName();
    }

    private void SetName()
    {
        //Ensures there are players
        if(GameInitializer.Instance.activePlayers.Count > 0)
        {
            for (int i = 0; i < GameInitializer.Instance.activePlayers.Count; i++)
            {
                //Sets player names

                allPlayersInfoHolder[i].GetComponent<PlayerGameplayUI>().playerName.text = UIControl.Instance.players[i].playerName;

                
                float healthPercentage = GetPercentageHealth(i);
                if (healthPercentage <= 0)
                {
                    GameInitializer.Instance.activePlayers[i].isActive = false;
                }

                //Fill Hearts based on health value
                SetIndividualHeartFill(healthPercentage, allPlayersInfoHolder[i].GetComponent<PlayerGameplayUI>().heartFills);

                //Disply this characters Avatar
                allPlayersInfoHolder[i].GetComponent<PlayerGameplayUI>().avatar.texture = GameInitializer.Instance.allPlayercustomizer[i].renderTexture;

                //Shows power Up Icons on health Bar
                EnableDisablePowerUpIcons(i);
            }
        }
        
    }
    private void SetIndividualHeartFill(float percentage, Image[] fills)
    {
        float firstFill = Mathf.Clamp(percentage, 0f, 0.33f);
        firstFill /= 0.3f;
        float SecondFill = Mathf.Clamp(percentage, 0.33f, 0.666f);
        SecondFill -= 0.333f;
        SecondFill /= 0.3f;
        float thirdFill = Mathf.Clamp(percentage, 0.66f, 1f);
        thirdFill -= 0.666f;
        thirdFill /= 0.333f;
        fills[0].fillAmount = firstFill;
        fills[1].fillAmount = SecondFill;
        fills[2].fillAmount = thirdFill;
    }

    private void EnableDisablePowerUpIcons(int playerIndex)
    {
        //Enables or disables powerUp icons if this player has any powerUp Active

        if (GameInitializer.Instance.activePlayers[GetNonNullInt(playerIndex)].player.gameObject.GetComponent<DuckControls>().isShieldActive)
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().shieldImg.gameObject.SetActive(true);
        }
        else
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().shieldImg.gameObject.SetActive(false);
        }
        if (GameInitializer.Instance.activePlayers[GetNonNullInt(playerIndex)].player.gameObject.GetComponent<DuckControls>().isSpeedActive)
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().speedImg.gameObject.SetActive(true);
        }
        else
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().speedImg.gameObject.SetActive(false);
        }
        if (GameInitializer.Instance.activePlayers[GetNonNullInt(playerIndex)].player.gameObject.GetComponent<DuckControls>().isScaleUpActive)
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().scaleUpImg.gameObject.SetActive(true);
        }
        else
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().scaleUpImg.gameObject.SetActive(false);
        }
        if (GameInitializer.Instance.activePlayers[GetNonNullInt(playerIndex)].player.gameObject.GetComponent<DuckControls>().isScaleDownActive)
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().scaleDownImg.gameObject.SetActive(true);
        }
        else
        {
            allPlayersInfoHolder[GetNonNullInt(playerIndex)].GetComponent<PlayerGameplayUI>().scaleDownImg.gameObject.SetActive(false);
        }
    }

    private float GetPercentageHealth(int index)
    {
        //Return a Player if its non null
        return (float)GameInitializer.Instance.activePlayers[GetNonNullInt(index)].player.gameObject.transform.GetChild(0).gameObject.GetComponent<DamageController>().health / 100f;
    }

    private int GetNonNullInt(int i)
    {
        //Ensures null chect for player
        
        while(GameInitializer.Instance.activePlayers[i].player == null)
        {
            i++;
            if(i == GameInitializer.Instance.activePlayers.Count)
            {
                if(GameInitializer.Instance.activePlayers.Count == 0)
                {
                    return 0;
                }
                else
                {
                    i = 0;
                }
                
            }
        }
        return i;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverMenu;
    public GameObject Pipe;
    public GameObject NarrowPipe;

    public Text Score;

    public Text GameOverScore;
    public Text HighScore;
    public GameObject NewHighScore;

    private float leftMostValue = -1.21f;
    private float rightMostValue = 1.56f;

    public float DelayBetweenTwoPipes = 1f;

    public int score = 0;

    public void PlayAgainPressed(){
        SceneManager.LoadScene("Game");
    }

    public void GameOver(){
        Time.timeScale = 0f;
        GameOverMenu.SetActive(true);
        GameOverScore.text = "Score : "+score.ToString();
        if(score > PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore", score);
            NewHighScore.SetActive(true);
        }
        else{
            NewHighScore.SetActive(false);
        }
        HighScore.text = "HighScore : " + PlayerPrefs.GetInt("HighScore");
    }

    private void Start(){
        Time.timeScale = 1f;
        InstantiatePipes();
    }

    void Update(){
        Score.text = score.ToString();
    }

    private void InstantiatePipes(){
        int random = Random.Range(0, 6);

        if(random == 2){
            InstantiateNarrowPipe();
        }

        else{
            InstiateNormalPipe();
        }

        Invoke("InstantiatePipes", DelayBetweenTwoPipes);
    }

    private void InstantiateNarrowPipe(){
        float xPosition = Random.Range(leftMostValue, rightMostValue);
        Instantiate(NarrowPipe, new Vector3(xPosition, 10f, 0f), Quaternion.identity);
    }

    private void InstiateNormalPipe(){
        float xPosition = Random.Range(leftMostValue, rightMostValue);
        Instantiate(Pipe, new Vector3(xPosition, 10f, 0f), Quaternion.identity);
    }
}

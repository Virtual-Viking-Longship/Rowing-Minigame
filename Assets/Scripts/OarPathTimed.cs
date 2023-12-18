using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OarPathTimed : MonoBehaviour
{
    // helps the oar path locate itself (to disable the game object) and the oars
    public GameObject oarPath;
    public GameObject[] phantomOars;
    public PhantomOarTimed[] phantomOarComponents;

    // variables and events that keep track of what point in the experience the user has reached
    public delegate void ResetOars();
    public static event ResetOars OnResetOars;
    public delegate void NextRound();
    public static event NextRound OnNextRound;
    int score = 0;
    bool tutorial = true;
    int round;
    float time = 0f;

    // ui elements to control
    public GameObject learnForward;
    public GameObject learnBackward;
    public GameObject forwardTime;
    public GameObject backwardTime;
    public GameObject gameText;
    public GameObject directionScore;
    public TextMeshProUGUI directionText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        learnForward.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Sequence();
    }

    void OnDisable()
    {

    }


    void Sequence()
    {
        learnForward.SetActive(false);
        time = 0f;
        // forwardTime.SetActive(true);
        // directionScore.SetActive(true);
        // probably not the most elegant way but will work for experimenting
        for (int i = 0; i < 5; i++)
        {
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                oar.Appear();
                while (time < 1)
                {
                    time += Time.deltaTime;
                }
                oar.Hit();
                time = 0f;
            }
        }
        

            forwardTime.SetActive(false);
            ResetScore();
            tutorial = false;
            // indicate that the game is starting with ui
            ResetGame();
    }

    // the script on the oars uses this method to add points based on the timer
    public void AddScore(int points)
    {
        score += points;
        scoreText.SetText(score.ToString());
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.SetText(score.ToString());
    }

    // this runs the game
    void Game()
    {
        gameText.SetActive(true);
        if (round == 0)
        {
            ResetScore();
        }
        else if (round == 5)
        {
            EndGame();
            oarPath.SetActive(false);
        }
        round++;
    }

    void EndGame()
    {
        // take score and initials and save them if in top 10, display leaderboard
    }

    // resets the score and starts the game again
    public void ResetGame()
    {
        ResetScore();
        round = 0;
        Game();
    }
}

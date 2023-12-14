using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OarPathTimed : MonoBehaviour
{
    // helps the oar path locate itself (to disable the game object) and the oars
    public GameObject oarPath;
    public GameObject[] phantomOars;
    public PhantomOar[] phantomOarComponents;

    // variables and events that keep track of what point in the experience the user has reached
    int resetCount;
    public delegate void ResetOars();
    public static event ResetOars OnResetOars;
    public delegate void NextRound();
    public static event NextRound OnNextRound;
    int score = 0;
    bool tutorial = true;
    int round;

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
        Forward();
        resetCount = 0;
        learnForward.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        OnResetOars += Sequence;
        OnNextRound += Game;
    }

    void OnDisable()
    {
        OnResetOars -= Sequence;
        OnNextRound -= Game;
    }

    // invoked by CheckOars, will make all oars active again
    // and reset the values of prevOarActive for all of them
    void ActivateOars()
    {
        for (int i = 0; i < phantomOars.Length; i++)
        {
            phantomOars[i].SetActive(true);
        }
        for (int i = 0; i < phantomOarComponents.Length; i++)
        {
            phantomOarComponents[i].prevOarActive = true;
        }
        phantomOarComponents[0].prevOarActive = false;
        resetCount++;
        // figures out if the user is playing the game or learning how to row
        if (tutorial)
        {
            if (OnResetOars != null)
            {
                OnResetOars();
                // goes to next round of tutorial
            }
        } 
        else
        {
            if (OnNextRound != null)
            {
                OnNextRound();
                // goes to next round of game
            }
        }
        
    }

    void Sequence()
    {
        // probably not the most elegant way but will work for experimenting
        if (resetCount == 3)
        {
            learnForward.SetActive(false);
            forwardTime.SetActive(true);
            directionScore.SetActive(true);
        } else if (resetCount == 6)
        {
            forwardTime.SetActive(false);
            ResetScore();
            tutorial = false;
            // indicate that the game is starting with ui
            ResetGame();
        }
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
        Forward();
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

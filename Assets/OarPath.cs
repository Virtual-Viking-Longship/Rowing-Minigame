using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OarPath : MonoBehaviour
{
    public GameObject oarPath;
    public GameObject[] phantomOars;

    int resetCount;
    public delegate void ResetOars();
    public static event ResetOars OnResetOars;
    public delegate void NextRound();
    public static event NextRound OnNextRound;
    int score = 0;
    bool tutorial = true;
    int round;

    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI turtorialBool;
    public TextMeshProUGUI roundNumber;
    public TextMeshProUGUI direction;
    public TextMeshProUGUI gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Forward();
        resetCount = 0;
        turtorialBool.SetText("true");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        PhantomOar.OnOarHit += CheckOars;
        OnResetOars += Sequence;
        OnNextRound += Game;
    }

    void OnDisable()
    {
        PhantomOar.OnOarHit -= CheckOars;
        OnResetOars -= Sequence;
        OnNextRound -= Game;
    }

    // checks to see if any oars are active, if not, activates all of them
    // has a short delay so user can see the last oar disappear
    // called by hitting an oar
    void CheckOars()
    {
        int count = 0;
        foreach (GameObject oar in phantomOars)
        {
            if (oar.activeInHierarchy == true)
            {
                count++;
            }
        }
        if (count == 0)
        {
            Invoke("ActivateOars", 0.2f);
        }
    }

    void ActivateOars()
    {
        foreach (GameObject oar in phantomOars)
            {
                oar.SetActive(true);
                oar.GetComponent<PhantomOar>().prevOarActive = true;
            }
            phantomOars[0].GetComponent<PhantomOar>().prevOarActive = false;
        resetCount++;
        if (tutorial)
        {
            OnResetOars();
        } 
        else
        {
            OnNextRound();
        }
        
    }

    // sets target values for rowing "forward"- ie the user moves the oar away from them first
    void Forward()
    {
        for (int i = 0; i < phantomOars.Length - 1; i++)
        {
            phantomOars[i].GetComponent<PhantomOar>().nextOar = phantomOars[i+1].GetComponent<PhantomOar>();
        }
        for (int i = 1; i < phantomOars.Length; i++)
        {
            phantomOars[i].GetComponent<PhantomOar>().prevOarActive = true;
        }
    }

    // sets target values for rowing "backward"- ie the user moves the oar toward them first
    void Backward()
    {
        phantomOars[0].GetComponent<PhantomOar>().nextOar = phantomOars[phantomOars.Length - 1].GetComponent<PhantomOar>();
        for (int i = phantomOars.Length - 1; i > 0; i--)
        {
            phantomOars[i].GetComponent<PhantomOar>().nextOar = phantomOars[i-1].GetComponent<PhantomOar>();
        }
        for (int i = 1; i < phantomOars.Length; i++)
        {
            phantomOars[i].GetComponent<PhantomOar>().prevOarActive = true;
        }
    }

    void Sequence()
    {
        // probably not the most elegant way but will work for experimenting
        if (resetCount == 6)
        {
            Backward();
        } else if (resetCount == 11)
        {
            Forward();
            score = 0;
        } else if (resetCount == 16)
        {
            Backward();
            score = 0;
        } else if (resetCount > 20) 
        { 
            tutorial = false;
            turtorialBool.SetText("false");
            // indicate that the game is starting with ui
            ResetGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreBoard.SetText(score.ToString());
    }

    void Game()
    {
        if (round == 0)
        {
            score = 0;
        }
        else if (round == 10)
        {
            EndGame();
            gameOver.SetText("Game Over!");
            oarPath.SetActive(false);
        }

        int random = Random.Range(0,2);
        if (random == 0)
        {
            direction.SetText("forward");
            Forward();
        }
        else
        {
            direction.SetText("backward");
            Backward();
        }
        round++;
        roundNumber.SetText(round.ToString());
    }

    void EndGame()
    {
        // take score and initials and save them if in top 10, display leaderboard
    }

    public void ResetGame()
    {
        score = 0;
        round = 0;
        Game();
    }
}

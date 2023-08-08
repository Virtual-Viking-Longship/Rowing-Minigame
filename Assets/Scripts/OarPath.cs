using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OarPath : MonoBehaviour
{
    public GameObject oarPath;
    public GameObject[] phantomOars;
    public PhantomOar[] phantomOarComponents;

    int resetCount;
    public delegate void DestroyOars();
    public static event DestroyOars OnOarsDestroyed;
    public delegate void ResetOars();
    public static event ResetOars OnResetOars;
    public delegate void NextRound();
    public static event NextRound OnNextRound;
    int score = 0;
    bool tutorial = true;
    int round;

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
        PhantomOar.OnOarHit += CheckOars;
        OnOarsDestroyed += ActivateOars;
        OnResetOars += Sequence;
        OnNextRound += Game;
    }

    void OnDisable()
    {
        PhantomOar.OnOarHit -= CheckOars;
        OnOarsDestroyed -= ActivateOars;
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
            //Invoke("ActivateOars", 0.2f);
            OnOarsDestroyed();
        }
    }

    void ActivateOars()
    {
        foreach (GameObject oar in phantomOars)
        {
            oar.SetActive(true);
        }
        foreach (PhantomOar oar in phantomOarComponents)
        {
            oar.prevOarActive = true;
        }
        phantomOarComponents[0].prevOarActive = false;
        resetCount++;
        if (tutorial)
        {
            if (OnResetOars != null)
            {
                OnResetOars();
            }
        } 
        else
        {
            if (OnNextRound != null)
            {
                OnNextRound();
            }
        }
        
    }

    // sets target values for rowing "forward"- ie the user moves the oar away from them first
    void Forward()
    {
        directionText.SetText("Forward");
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
        directionText.SetText("Backward");
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
            learnForward.SetActive(false);
            learnBackward.SetActive(true);
            Backward();
        } else if (resetCount == 11)
        {
            learnBackward.SetActive(false);
            forwardTime.SetActive(true);
            directionScore.SetActive(true);
            Forward();
            ResetScore();
        } else if (resetCount == 16)
        {
            forwardTime.SetActive(false);
            backwardTime.SetActive(true);
            Backward();
            ResetScore();
        } else if (resetCount > 20) 
        { 
            backwardTime.SetActive(false);
            tutorial = false;
            // indicate that the game is starting with ui
            ResetGame();
        }
    }

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

    void Game()
    {
        gameText.SetActive(true);
        if (round == 0)
        {
            ResetScore();
        }
        else if (round == 10)
        {
            EndGame();
            oarPath.SetActive(false);
        }

        int random = Random.Range(0,2);
        if (random == 0)
        {
            Forward();
        }
        else
        {
            Backward();
        }
        round++;
    }

    void EndGame()
    {
        // take score and initials and save them if in top 10, display leaderboard
    }

    public void ResetGame()
    {
        ResetScore();
        round = 0;
        Game();
    }
}

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

    // score variable
    int score = 0;

    // ui elements to control
    public GameObject learnForward;
    public GameObject forwardTime;
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
        StartCoroutine(Sequence());
    }

    void OnDisable()
    {

    }

    IEnumerator Sequence()
    {
        learnForward.SetActive(true);
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 3; i++)
        {
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                oar.Appear();
                yield return new WaitForSeconds(0.5f);
                oar.Hit();
            }
        }
        ResetScore();
        learnForward.SetActive(false);
        forwardTime.SetActive(true);
        directionScore.SetActive(true);
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 3; i++)
        {
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                oar.Appear();
                yield return new WaitForSeconds(0.5f);
                oar.Hit();
            }
        }
        yield return new WaitForSeconds(5);
        ResetScore();
        forwardTime.SetActive(false);
        gameText.SetActive(true);
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 10; i++)
        {
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                oar.Appear();
                yield return new WaitForSeconds(0.5f);
                oar.Hit();
            }
        }
    }

    // the script on the oars uses this method to add points
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
}

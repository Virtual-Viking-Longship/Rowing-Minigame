using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OarPathTimed : MonoBehaviour
{
    // helps the oar path locate itself (to disable the game object) and the oars
    public GameObject oarPath;
    public GameObject[] phantomOars;
    // TO DO: make it so that you just have to assign the gameobjects in the editor
    // and then in start it goes through and gets these components
    public PhantomOarTimed[] phantomOarComponents;

    // score variable- can this be private? does it matter?
    int score = 0;

    // ui elements to control
    public GameObject learnForward;
    public GameObject forwardTime;
    public GameObject gameText;
    public GameObject directionScore;
    public TextMeshProUGUI scoreText;
    public GameObject end;

    // stroke time
    public float time = 3f;
    private float stroke;
    private float recovery;
    private int split;
    private float perStroke;
    private float perRecovery;

    // Start is called before the first frame update
    void Start()
    {
        // this ui activation seems redundant but leaving for now until there is time to make sure 
        // removing it doesnt break anything
        learnForward.SetActive(true);

        // figure out how many oar targets there are
        int oarCount = phantomOars.Length;
        // replaced magic numbers- confirm function week 4
        stroke = time / oarCount;
        recovery = 2 * time / oarCount;
        split = oarCount / 2;
        perStroke = stroke / split;
        perRecovery = recovery / split;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        // Co-routines let you put in delays in a few ways, in this case, by number of seconds
        StartCoroutine(Sequence());
    }

    void OnDisable()
    {

    }

    IEnumerator Sequence()
    {
        // tells the WaitForSeconds how long to pause, different for oars in stroke vs recovery
        float wait;
        // activate the UI for the first set of instructions
        learnForward.SetActive(true);
        // wait so the user can read- eventually there will also be audio for them to listen to-
        // may have to adjust the timing based on audio and user testing
        yield return new WaitForSeconds(5);
        // first stage has 3 rounds
        for (int i = 0; i < 3; i++)
        {
            // goes through the array of oars
            // if the stroke and recovery ever end up uneven this will need to be split into 2 loops
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                // figure out of this target is part of stroke or recovery and assign delay accordingly
                if (oar.stroke)
                {
                    wait = perStroke;
                }
                else
                {
                    wait = perRecovery;
                }
                // enables the target and does some other bookkeeping
                oar.Appear();
                // delays the appearance of the next oar
                yield return new WaitForSeconds(wait);
            }
            // a break before disabling the targets so the user can try to catch up
            yield return new WaitForSeconds(3);
            // disable all of the targets at once
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                // disables the target and does some other bookkeeping
                oar.Hit();
            }
            // quick break between rounds
            yield return new WaitForSeconds(1);
        }
        // reset the score and enable the proper UI elements for stage 2
        ResetScore();
        learnForward.SetActive(false);
        forwardTime.SetActive(true);
        directionScore.SetActive(true);
        // again, a break for reading/listening, time may need adjusting
        yield return new WaitForSeconds(5);

        // second stage has 3 rounds
        for (int i = 0; i < 3; i++)
        {
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                if (oar.stroke)
                {
                    wait = perStroke;
                }
                else
                {
                    wait = perRecovery;
                }
                oar.Appear();
                yield return new WaitForSeconds(wait);
                // now the oars will disappear as the next one appears- so timing matters
                oar.Hit();
            }
        }
        // break so the user can see their score
        yield return new WaitForSeconds(5);
        // reset the score and enable the UI elements for stage 3
        ResetScore();
        forwardTime.SetActive(false);
        gameText.SetActive(true);
        // again, a break for reading/listening, time may need adjusting
        yield return new WaitForSeconds(5);

        // stage 3 is the challenge stage, currently 10 rounds, refine with testing
        for (int i = 0; i < 10; i++)
        {
            foreach(PhantomOarTimed oar in phantomOarComponents)
            {
                if (oar.stroke)
                {
                    wait = perStroke;
                }
                else
                {
                    wait = perRecovery;
                }
                oar.Appear();
                yield return new WaitForSeconds(wait);
                oar.Hit();
            }
        }
        // final UI changes to congratulate user and display their score
        gameText.SetActive(false);
        end.SetActive(true);
    }

    // the script on the targets uses this method to add points
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

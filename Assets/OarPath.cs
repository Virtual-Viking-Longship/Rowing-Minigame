using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarPath : MonoBehaviour
{
    public GameObject[] phantomOars;

    int resetCount;
    public delegate void ResetOars();
    public static event ResetOars OnResetOars;

    // Start is called before the first frame update
    void Start()
    {
        Forward();
        resetCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        PhantomOar.OnOarHit += CheckOars;
        OnResetOars += Sequence;
    }

    void OnDisable()
    {
        PhantomOar.OnOarHit -= CheckOars;
        OnResetOars -= Sequence;
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
        OnResetOars();
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
            // timing
        } else if (resetCount == 16)
        {
            Backward();
            // timing
        } else if (resetCount > 20) 
        {
            // game
        }
    }
}

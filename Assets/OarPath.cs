using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarPath : MonoBehaviour
{
    public GameObject[] phantomOars;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        PhantomOar.OnOarHit += CheckOars;
    }

    void OnDisable()
    {
        PhantomOar.OnOarHit -= CheckOars;
    }

    // checks to see if any oars are active, if not, activates all of them
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
    }
}

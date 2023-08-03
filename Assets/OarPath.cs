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
        PhantomOar.OnOarHit += ActivateOars;
    }

    void OnDisable()
    {
        PhantomOar.OnOarHit -= ActivateOars;
    }

    // checks to see if any oars are active, if not, activates all of them
    // called by hitting an oar
    void ActivateOars()
    {
        foreach (GameObject oar in phantomOars)
        {
            if (oar.activeInHierarchy == true)
            {
                break;
            }
        }
        foreach (GameObject oar in phantomOars)
        {
            oar.SetActive(true);
        }
    }
}

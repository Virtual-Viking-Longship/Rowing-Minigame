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

    // makes all the oars active- call every time an oar is hit? using events?
    void ActivateOars()
    {
        foreach (GameObject oar in phantomOars)
        {
            oar.SetActive(true);
        }
    }
}

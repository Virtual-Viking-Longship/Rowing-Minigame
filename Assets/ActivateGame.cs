using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ActivateGame : MonoBehaviour
{
    private bool start = false;

    private HandGrabInteractor leftGrab;
    private HandGrabInteractor rightGrab;
    public GameObject oarPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InteractorState.Select == leftGrab.State & start) 
        {
            oarPath.SetActive(true);
            start = false;
        }
    }
}

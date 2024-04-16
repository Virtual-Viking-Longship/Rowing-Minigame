using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhantomOarTimed : MonoBehaviour
{
    // want to rename this at some point when there is more time to troubleshoot the inevitable
    // missed reference to change
    public GameObject space;
    public Collider grabOarCollider;
    // might be able to make this private and initialize it in Start or OnEnable
    public PhantomOarTimed nextOar;
    // probably should be private
    public bool prevOarActive;
    // does not appear to be referenced anywhere anymore, will try removing when there is time to fix
    // things in case it breaks something
    public bool firstOar;
    // these should be assigned in the editor still
    public OarPathTimed oarPath;
    public bool stroke;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // make sure this is the oar hitting it
        // make sure it is being hit in the right order (after the one before it)
        if (other == grabOarCollider && !prevOarActive) 
        {
            Hit();
            oarPath.AddScore(10);
        }
    }

    public void Hit()
    {
        space.SetActive(false);
        nextOar.prevOarActive = false;
    }

    public void Appear()
    {
        space.SetActive(true);
        nextOar.prevOarActive = true;
    }
}

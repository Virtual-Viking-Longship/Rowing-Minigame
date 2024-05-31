using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhantomOarTimed : MonoBehaviour
{
    // OarPathTimed uses methods to assign these
    private Collider grabOarCollider;
    private PhantomOarTimed nextOar;
    private bool prevOarActive;
    private OarPathTimed oarPath;
    // this one could be private if decide not to allow for uneven stroke/recovery
    // currently OarPathTimed assumes they are even, but if decide to make it possible to do uneven
    // then this will need to stay public
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
        gameObject.SetActive(false);
        nextOar.prevOarActive = false;
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        nextOar.prevOarActive = true;
    }

    public void AssignNextOar(PhantomOarTimed oar) {
        nextOar = oar;
    }

    public void AssignOarPath(OarPathTimed oarPath1) {
        oarPath = oarPath1;
    }

    public void AssignGrabOarCollider(Collider grabOar) {
        grabOarCollider = grabOar;
    }
}

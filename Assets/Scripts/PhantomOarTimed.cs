using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhantomOarTimed : MonoBehaviour
{
    public GameObject space;
    public Collider grabOarCollider;
    public PhantomOarTimed nextOar;
    public bool prevOarActive;
    public bool firstOar;
    public OarPathTimed oarPath;

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
        // with new training round should also check if the previous oar is active, 
        // otherwise could do wrong order
        if (other == grabOarCollider) 
        {
            Hit();
            oarPath.AddScore(10);
        }
    }

    public void Hit()
    {
        space.SetActive(false);
    }

    public void Appear()
    {
        space.SetActive(true);
    }
}

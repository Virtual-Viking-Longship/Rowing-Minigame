using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomOar : MonoBehaviour
{
    public GameObject space;
    public Collider grabOarCollider;
    //public PhantomOar prevOar;
    public PhantomOar nextOar;
    public bool prevOarActive;

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
        if (!prevOarActive && other == grabOarCollider)
        {
            SetInactive();
        }
    }

    void SetInactive()
    {
        nextOar.prevOarActive = false;
        space.SetActive(false);
    }
}

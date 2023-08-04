using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhantomOar : MonoBehaviour
{
    public GameObject space;
    public Collider grabOarCollider;
    public PhantomOar nextOar;
    public bool prevOarActive;

    public delegate void OarHit();
    public static event OarHit OnOarHit;

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
            if (OnOarHit != null)
            {
                OnOarHit();
            }
            }
    }

    void SetInactive()
    {
        nextOar.prevOarActive = false;
        space.SetActive(false);
    }
}

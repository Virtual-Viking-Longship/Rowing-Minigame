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
    public bool firstOar;
    public OarPath oarPath;

    public delegate void OarHit();
    public static event OarHit OnOarHit;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!prevOarActive)
        {
            time += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!prevOarActive && other == grabOarCollider)
        {
            SetInactive();
            oarPath.AddScore(5);
            if (time < 10 && time > 3 && !firstOar) 
            {
                oarPath.AddScore(10);
            } 
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

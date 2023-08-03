using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhantomOar : MonoBehaviour
{
    public TextMeshProUGUI display;
    public TextMeshProUGUI display2;
    public TextMeshProUGUI display3;
    public TextMeshProUGUI display4;
    public string oarNumber;
    public GameObject space;
    public Collider grabOarCollider;
    //public PhantomOar prevOar;
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
        display.SetText(oarNumber);
        display2.SetText(other.name);
        display3.SetText(grabOarCollider.name);
        if (!prevOarActive && other == grabOarCollider)
        {
            display4.SetText("if condition");
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

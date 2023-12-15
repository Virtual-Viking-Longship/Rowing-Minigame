using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhantomOarTimed : MonoBehaviour
{
    public GameObject space;
    public Collider grabOarCollider;
    public PhantomOar nextOar;
    public bool prevOarActive;
    public bool firstOar;
    public OarPath oarPath;

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
        oarPath.AddScore(10);
        Hit();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class ReloadGame : MonoBehaviour
{
    public GameObject loader;
 
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Two)){
            loader.GetComponent<LoadAllScenes>().Reload();
        }
    }
}
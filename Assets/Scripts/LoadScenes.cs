using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public string[] sceneNames;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sceneNames.Length; i++)
        {
            SceneManager.LoadScene(sceneNames[i], LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAllScenes : MonoBehaviour
{
    // this script as written is not ideal for when the project gets put into one app
    // i have started looking into a way to assign scenes from the editor but have not had any success yet
    // this is something to look into more before trying to build the project to launch from a main menu
    public string[] sceneNames;
    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < sceneNames.Length; i++)
        {
            SceneManager.LoadScene(sceneNames[i], LoadSceneMode.Additive);
        }*/
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reload()
    {
        /*for (int i = 0; i < sceneNames.Length; i++) 
        {
            SceneManager.UnloadSceneAsync(sceneNames[i]);
        }*/
        SceneManager.UnloadSceneAsync(1);
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        /*for (int i = 0; i < sceneNames.Length; i++)
        {
            SceneManager.LoadScene(sceneNames[i], LoadSceneMode.Additive);
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Scripts that allows us to move between the two scenes
    public string SceneName1;
    public string SceneName2;

    public void ChangeSceneNext() { 
        SceneManager.LoadScene(SceneName2);
    }

    public void ChangeScenePrev()
    {
        SceneManager.LoadScene(SceneName1);
    }
}

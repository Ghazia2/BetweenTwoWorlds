using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private string sceneName;
    public void StartSceneChange(string sceneToChange)
    {
        sceneName = sceneToChange;
    }

    public void ChangeScene()
    {      
        FindObjectOfType<ScreenFader>().FadeSceneOut();
        SceneManager.LoadScene(sceneName);
    }
}

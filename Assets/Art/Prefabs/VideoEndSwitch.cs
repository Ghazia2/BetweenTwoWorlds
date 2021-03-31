using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndSwitch : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public string SceneName;
    public string videoName;
    public bool noBars;
    public bool showUI = false;
    void Start()
    {
        Cursor.visible = false;
        VideoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        VideoPlayer.Play();
        if (noBars)
            VideoPlayer.loopPointReached += LoadScenes;
        if(!noBars)
            VideoPlayer.loopPointReached += LoadScene;
    }
    void LoadScene(VideoPlayer vp)
    {
        GameManager.SwitchScenes2(SceneName);
        FindObjectOfType<AudioManager>().Play("FadeOutDrop");
        if (showUI)
            DisplayManager.UnHideUI();
    }
    void LoadScenes(VideoPlayer vp)
    {
        GameManager.SwitchScenes(SceneName);
        FindObjectOfType<AudioManager>().Play("FadeOutDrop");
        if (showUI)
            DisplayManager.UnHideUI();
        if (!showUI)
            DisplayManager.HideUI();
    }
}

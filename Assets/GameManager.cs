using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private string sceneName;
    public float xPosition = -5.3f;
    public float yPosition = -1.9f;
    public int numberOfDeaths = 0;
    public float deathSequenceDuration = 3f;
    public Vector2 lastCheckPointPos;
    public int tempOrbCount = 0;
    public int actualOrbCount = 0;
    List<Collectitem> orbs;
    ScreenFader screenFader;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        orbs = new List<Collectitem>();
        DontDestroyOnLoad(gameObject);

    }

    public static void RegisterSceneFader(ScreenFader fader)
    {
        if (instance == null)
            return;
        instance.screenFader = fader;
    }

    public static void RegisterOrb(Collectitem orb)
    {
        if (instance == null)
            return;
        if (!instance.orbs.Contains(orb))
            instance.orbs.Add(orb);
        //DisplayManager.UpdateOrbUI(instance.orbs.Count);
    }

    public static void RegisterOrbCollected(Collectitem orb)
    {
        if (instance == null)
            return;
        if (!instance.orbs.Contains(orb))
            return;
        instance.orbs.Remove(orb);
        instance.tempOrbCount++;
        DisplayManager.UpdateOrbUI(instance.tempOrbCount);
    }
    public static void PlayerDied()
    {
        if (instance == null)
            return;
        if (instance.screenFader != null)
            instance.screenFader.FadeSceneOut();
        instance.numberOfDeaths++;
        DisplayManager.UpdateDeathUI(instance.numberOfDeaths);
        instance.Invoke("Restart", instance.deathSequenceDuration);
    }
    public static void PlayerDiedWithoutDelay()
    {
        if (instance == null)
            return;
        if (instance.screenFader != null)
            instance.screenFader.FadeSceneOut();
        instance.numberOfDeaths++;
        DisplayManager.UpdateDeathUI(instance.numberOfDeaths);
        instance.Invoke("Restart", 0f);
    }
    public static void SwitchScenes(string sceneToChange)
    {
        if (instance == null)
            return;
        if (instance.screenFader != null)
            instance.screenFader.FadeSceneOut();
        instance.sceneName = sceneToChange;
        instance.Invoke("ChangeScenes", 0.75f);
        instance.lastCheckPointPos = new Vector2(instance.xPosition, instance.yPosition);
    }
    public static void SwitchScenes2(string sceneToChange)
    {
        if (instance == null)
            return;                  
        instance.sceneName = sceneToChange;
        instance.Invoke("ChangeScenes", 0f);
        instance.lastCheckPointPos = new Vector2(instance.xPosition, instance.yPosition);       
        instance.Invoke("CutsceneAdustments", 0.037f);
    }

    void Restart()
    {
        orbs.Clear();
        ResetActualOrbCount();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void CutsceneAdustments()
    {
        instance.screenFader.NoFade();
    }
    void ChangeScenes()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void UpdateActualOrb()
    {
        actualOrbCount = tempOrbCount;
    }
    void ResetActualOrbCount()
    {
        tempOrbCount = actualOrbCount;
    }
}

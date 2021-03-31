using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
    }
    public void StartMenu()
    {
        AudioManager.instance.Play("MainTheme");
        AudioManager.instance.Stop("AmbientSound");
        GameManager.SwitchScenes("Intro");
        GameManager.instance.tempOrbCount = 0;
        GameManager.instance.actualOrbCount = 0;
    }
    public void CreditsMenu()
    {
        GameManager.SwitchScenes("Credits");
    }
    public void ExitMenu()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        DisplayManager.UpdateDeathUI(0);
        GameManager.instance.numberOfDeaths = 0;
        AudioManager.instance.Stop("MainTheme");
        AudioManager.instance.Play("AmbientSound");
        GameManager.SwitchScenes("TitleMenu");
    }
}

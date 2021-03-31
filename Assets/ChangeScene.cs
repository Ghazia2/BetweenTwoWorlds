using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToChange;
    public float xPos;
    public float yPos;
    private GameManager gm;
    public bool showUI = false;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.xPosition = xPos;
            GameManager.instance.yPosition = yPos;
            GameManager.SwitchScenes(sceneToChange);
            if (showUI)
                DisplayManager.UnHideUI();
            if (!showUI)
                DisplayManager.HideUI();
        }
    }
}

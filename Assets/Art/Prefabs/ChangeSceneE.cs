using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneE : MonoBehaviour
{
    public string sceneToChange;
    private GameManager gm;
    public bool canChangeScene = false;
    public float xPos;
    public float yPos;
    public bool showUI = false;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
      
        
    }
    private void Update()
    {
        ChangeScreen();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canChangeScene = true;
        }
    }
    private void ChangeScreen()
    {
        if(canChangeScene && Input.GetKey(KeyCode.E))
        {
            GameManager.instance.xPosition = xPos;
            GameManager.instance.yPosition = yPos;
            GameManager.SwitchScenes(sceneToChange);
            if (showUI)
                DisplayManager.UnHideUI();
            if (!showUI)
                DisplayManager.HideUI();
            canChangeScene = false;
        }        
    }
}

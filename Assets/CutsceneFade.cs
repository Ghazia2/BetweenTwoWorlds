using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFade : MonoBehaviour
{
    public string sceneToChange;
    private GameManager gm;
    private Animator anim;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.SwitchScenes2(sceneToChange);
        }
    }
}

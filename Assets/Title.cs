using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Invoke("SwitchS", 3f);
    }
    void SwitchS()
    {
        GameManager.SwitchScenes("Starting");
    }
}

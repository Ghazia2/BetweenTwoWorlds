using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntructionManager : MonoBehaviour
{
    private Animator anim;
    public KeyCode keyName;
    public KeyCode keyName2;
    public bool shoot = false;
    public float slowmoDuration = 2f;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKey(keyName) || Input.GetKey(keyName2))
        {
            anim.SetTrigger("Done");
            if (shoot)
            {
                FindObjectOfType<TimeManager>().slowdownLength = slowmoDuration;
                FindObjectOfType<TimeManager>().DoSlowMotion();
            }
        }           
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.RegisterSceneFader(this);
        
    }

    public void FadeSceneOut()
    {
        anim.SetTrigger("Fade");
    }
    public void NoFade()
    {
        anim.SetTrigger("NoFade");
    }

}

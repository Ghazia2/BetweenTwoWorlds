using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerV2 : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayableDirectorScript()
    {
        PlayableDirector playableDirector = GetComponent<PlayableDirector>();
        playableDirector.Play();
        transform.position = new Vector2(48f, -3.02f);
    }
    public void ChangeScale()
    {
        transform.Rotate(0, -180, 0);
    }
    public void PlayDeathAnimation()
    {
        anim.SetTrigger("Dead");
    }
}

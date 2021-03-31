using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeLight : MonoBehaviour
{
    private Animator anim;
    public AudioSource audioSource;
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    public void TubeLightFall()
    {
        anim.SetTrigger("Fall");
        audioSource.Stop();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().Die();
            GameManager.PlayerDied();
        }
    }
}

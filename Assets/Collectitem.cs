using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectitem : MonoBehaviour
{
    private Animator anim;
    public int orbCount = 0;
    private void Start()
    {
        GameManager.RegisterOrb(this);
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            orbCount ++;
            GameManager.RegisterOrbCollected(this);
            FindObjectOfType<AudioManager>().Play("OrbCollection");
            anim.SetTrigger("Burst");
            FindObjectOfType<AudioManager>().Play("OrbReturn");
        }
    }
    public void SetActive()
    {
        gameObject.SetActive(false);
    }
}

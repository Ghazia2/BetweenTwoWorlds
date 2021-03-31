using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timelien : MonoBehaviour
{
    public float slowmoDuration;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerV2>().PlayableDirectorScript();
            FindObjectOfType<TimeManager>().slowdownLength = slowmoDuration;
            FindObjectOfType<TimeManager>().DoSlowMotion();
            AudioManager.instance.Play("PlotTwist");
            Destroy(gameObject);
        }           
    }
}

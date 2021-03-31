using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopDetector2 : MonoBehaviour
{
    public GameObject UI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            UI.SetActive(false);
    }
}

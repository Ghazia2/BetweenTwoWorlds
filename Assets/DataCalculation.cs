using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCalculation : MonoBehaviour
{
    public GameObject portal;
    public GameObject winPortal;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (GameManager.instance.actualOrbCount == 10)
                winPortal.SetActive(true);
            else
                portal.SetActive(true);
        }
    }
}

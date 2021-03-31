using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillDetector : MonoBehaviour
{
    public Boss[] thisEnemy;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Boss startMovingEnemy in thisEnemy)
            {
                startMovingEnemy.StartFight();
            }
            Destroy(gameObject);
        }
    }
}

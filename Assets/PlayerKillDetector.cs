using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillDetector : MonoBehaviour
{
    public Enemy2[] thisEnemy;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Enemy2 startMovingEnemy in thisEnemy)
            {
                startMovingEnemy.canMove = true;
                startMovingEnemy.canShoot = true;
            }
            Destroy(gameObject);
        }
    }
}

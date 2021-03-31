using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Enemy[] thisEnemy;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Enemy startMovingEnemy in thisEnemy)
            {
                startMovingEnemy.canMove = true;
            }
            Destroy(gameObject);
        }
        
    }
}

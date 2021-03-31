using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public float bulletLife;
    public Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody.velocity = transform.right * speed;
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (FindObjectOfType<PlayerMovement>().isDead)
                return;
            other.GetComponent<PlayerMovement>().Die();
            GameManager.PlayerDied();
            Destroy(gameObject);
        }
        if (other.CompareTag("Platforms"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rigidBody;
    public int damage;

    private void Start()
    {
        rigidBody.velocity = transform.right * speed;
        Destroy(gameObject,0.87f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().hit = true;
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy2"))
        {
            other.GetComponent<Enemy2>().hit = true;
            other.GetComponent<Enemy2>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy3"))
        {
            other.GetComponent<Boss>().TakeDamage(2*damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Platforms"))
        {
            Destroy(gameObject);
        }
        if(other.CompareTag("PlayerV2"))
        {
            FindObjectOfType<PlayerV2>().PlayDeathAnimation();
            AudioManager.instance.Play("DeathSound");
            Invoke("ChangeScenes", 0.4f);
        }
    }
    void ChangeScenes()
    {
        GameManager.SwitchScenes("DeadBodies");
        DisplayManager.HideUI();
    }
}

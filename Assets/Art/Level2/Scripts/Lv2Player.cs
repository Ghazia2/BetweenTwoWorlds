using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2Player : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    private bool isDead = false;
    private float direction = 1f;
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Sliding();
    }
    void Sliding()
    {
        if(isDead)
        {
            rb.velocity = new Vector2(0f, 0f);
            return;
        }
        float xVelocity = xSpeed * Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xVelocity, -ySpeed * 100 * Time.deltaTime);
        if (xVelocity * direction < 0f)
            FlipDirection();


    }
    void FlipDirection()
    {
        direction *= -1f;
        transform.Rotate(0f, 180f, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathColliders"))
        {
            Dead();
            GameManager.PlayerDied();
        }
        
    }
    void Dead()
    {
        isDead = true;
        anim.SetTrigger("dead");
    }
}

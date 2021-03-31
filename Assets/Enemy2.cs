using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int health = 100;
    public Transform groundCheck;
    public Transform HeadCheck;
    public Transform firePoint1;
    public Transform firePoint2;
    public Vector2 groundCheckSize;
    public Vector2 HeadCheckSize;
    public GameObject enemyBulletPrefab;
    private float direction = 1f;
    public float waitOnTurn = 2f;
    public float fireTime = 2f;
    public float speed;
    public bool canMove = false;
    public bool canShoot = false;
    public bool hit;
    public GameObject deathEffect;
    private Animator anim;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        if (canMove)
        {
            anim.SetBool("CanMove", true);
            transform.position += Vector3.left * direction * speed * Time.deltaTime;
            DetectFloor();
        }
        if (canShoot && !IsBlocked())
        {
            StartCoroutine(Shoot());
        }
        if (hit)
        {
            canMove = false;
            anim.SetBool("CanMove", false);
            anim.SetTrigger("Hit");
        }
    }

    void DetectFloor()
    {
        if ((!IsOnGround() && canMove) || IsBlocked())
        {
            canMove = false;
            StartCoroutine(ChangeTarget());
        }
    }

    IEnumerator ChangeTarget()
    {
        canMove = false;
        anim.SetBool("CanMove", false);
        yield return new WaitForSeconds(waitOnTurn);

        FlipDirection();
        canMove = true;
        anim.SetBool("CanMove", true);
    }

    void FlipDirection()
    {
        direction *= -1f;
        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator Shoot()
    {
        anim.SetTrigger("Shoot");
        Instantiate(enemyBulletPrefab, firePoint1.position, firePoint1.rotation);
        Instantiate(enemyBulletPrefab, firePoint2.position, firePoint2.rotation);
        canShoot = false;
        yield return new WaitForSeconds(fireTime);
        canShoot = true;
    }
    public void EverthingIsOkay()
    {
        hit = false;
        canMove = true;
        anim.SetBool("CanMove", true);
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    bool IsOnGround()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, LayerMask.GetMask("Platforms"));
    }

    bool IsBlocked()
    {
        return Physics2D.OverlapBox(HeadCheck.position, HeadCheckSize, 0f, LayerMask.GetMask("Platforms"));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawWireCube(HeadCheck.position, HeadCheckSize);
    }
}

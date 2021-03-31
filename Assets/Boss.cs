using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 100;
    public Transform groundCheck;
    public Transform HeadCheck;
    public Transform firePoint1;
    public Transform firePoint2;
    public Vector2 groundCheckSize;
    public Vector2 HeadCheckSize;
    public GameObject enemyBulletPrefab;
    public float waitOnTurn = 2f;
    public float fireTime = 2f;
    public float speed;
    public float lazarRate = 2f;
    public bool canMove = false;
    public bool canShoot = false;
    public bool isMortal = true;
    public bool hit;
    public GameObject deathEffect;
    public GameObject healthBar;
    private Animator anim;

    public void TakeDamage(int damage)
    {
        if (isMortal)
            return;
        health -= damage;
        if (health <= 900)
            anim.SetBool("IsEnraged", true);
        if (health <= 0)
        {
            Die();
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        FindObjectOfType<AudioManager>().Stop("BossLaser");
        FindObjectOfType<AudioManager>().Stop("VictoryClip1");
        FindObjectOfType<AudioManager>().Stop("VictoryClip2");
    }

    void Shoot()
    {
        Instantiate(enemyBulletPrefab, firePoint1.position, firePoint1.rotation);
        Instantiate(enemyBulletPrefab, firePoint2.position, firePoint2.rotation);
        /*canShoot = false;
        yield return new WaitForSeconds(fireTime);
        canShoot = true;*/
    }
    
    public void StartCourotine()
    {
        /*if(canShoot)
        {
            StartCoroutine(Shoot());
        }*/
        
    }
    public void StartFight()
    {
        anim.SetTrigger("StartIntro");
    }
    public void EverthingIsOkay()
    {
        hit = false;
        canMove = true;
        anim.SetBool("CanMove", true);
    }
    public void StartLazar()
    {
         anim.SetTrigger("EnragedLazar");

    }
    public void IsMortalON()
    {
        isMortal = true;
    }
    public void IsMortalOFF()
    {
        isMortal = false;
    }
    public void LazarOff()
    {
        anim.SetTrigger("LazarOff");
    }
    public void BossEntry()
    {
        FindObjectOfType<AudioManager>().Play("BossEntry");
    }
    public void BossTransformation()
    {
        FindObjectOfType<AudioManager>().Play("BossTranformation");
    }
    public void BossArm()
    {
        FindObjectOfType<AudioManager>().Play("BossArm");
    }

    public void ShowHealthBar()
    {
        healthBar.SetActive(true);
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("VictoryClip1");
        FindObjectOfType<AudioManager>().Play("VictoryClip2");
        Destroy(gameObject);
        FindObjectOfType<HealthBar>().DestroyHealthBar();
    }
    bool IsOnGround()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, LayerMask.GetMask("Platforms"));
    }

    public bool IsBlocked()
    {
        return Physics2D.OverlapBox(HeadCheck.position, HeadCheckSize, 0f, LayerMask.GetMask("Platforms"));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawWireCube(HeadCheck.position, HeadCheckSize);
    }
}

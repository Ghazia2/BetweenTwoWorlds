using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float ladderSpeed;
    public float crouchSpeed;
    public float jumpForce;
    public float fireRate = 15f;
    private float direction = 1f;
    private float nextTimeToFire = 0f;
    private float ladderPosition;
    public float distance;
    public bool isCrouching = false;
    public bool gotGun = false;
    public bool canShoot = true;
    public bool isRunning = false;
    public bool isClimbing = false;
    public bool isDead = false;
    public bool isPlayerActive = true;
    public Transform groundCheck;
    public Transform HeadCheck;
    public Transform firePoint;
    public Transform firePointCrouch;
    public GameObject bulletPrefab;
    public GameObject deathVFXPrefab;
    public Vector2 groundCheckSize;
    public Vector2 HeadCheckSize;
    Vector2 crouchBodySize;
    Vector2 crouchBodyOffset;
    Vector2 bodySize;
    Vector2 bodyOffset;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private GameManager gm;
    private AudioManager am;
    private GameObject go;
    public GameObject gun;

    void Start()
    {
        Cursor.visible = false;
        DisplayManager.UpdateOrbUI(GameManager.instance.tempOrbCount);
        rigidBody   = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        bodySize = boxCollider.size;
        bodyOffset = boxCollider.offset;
        crouchBodySize = new Vector2(boxCollider.size.x, boxCollider.size.y / 2f);
        crouchBodyOffset = new Vector2(boxCollider.offset.x, -0.53f);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        transform.position = gm.lastCheckPointPos;
    }
    void Update()
    {
        if (isPlayerActive)
        {
            GroundMovement();
            Jump();
            Crouch();
        }   
        if(!isPlayerActive)
        {
            rigidBody.velocity = new Vector2(0f, 0f);
            anim.SetFloat("xSpeed", 0f);
            anim.SetFloat("ySpeed", 0f);
            isRunning = false;
            if(IsOnGround())
                anim.SetBool("IsJumping", false);
        }
        Shoot();
        LadderCheck();
    }
    void FixedUpdate()
    {
        Climb();
    }
    void GroundMovement()
    {
        if (isDead)
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
            return;
        }
        float xVelocity = speed * Input.GetAxisRaw("Horizontal");
        anim.SetFloat("xSpeed", Mathf.Abs(xVelocity));
        anim.SetFloat("ySpeed", rigidBody.velocity.y);
        if (isCrouching)
        {
            xVelocity = crouchSpeed * Input.GetAxisRaw("Horizontal");
        }
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
        if(Mathf.Abs(xVelocity) > 0.01f)
            isRunning = true;
        if (Mathf.Abs(xVelocity) < 0.01f)
            isRunning = false;
        if (xVelocity * direction < 0f)
            FlipDirection();
    }

    void Jump()
    {
        if (isDead)
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
            return;
        }
        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            isCrouching = false;
            anim.SetBool("IsJumping", true);
            rigidBody.gravityScale = 3;
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            FindObjectOfType<AudioManager>().Play("Jump");
            FindObjectOfType<AudioManager>().Play("JumpVoice");
        }
        else if (!Input.GetButtonDown("Jump") && rigidBody.velocity.y < 0f)
        {
            anim.SetBool("IsJumping", true);
            rigidBody.gravityScale = 5;
        }
        else if (!Input.GetButtonDown("Jump") && rigidBody.velocity.y > 0f)
        {
            anim.SetBool("IsJumping", true);
            
        }
        if (!Input.GetButtonDown("Jump") && IsOnGround())
            anim.SetBool("IsJumping", false);
    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && IsOnGround())
        {
            isCrouching = true;
            anim.SetBool("Crouching", true);
            boxCollider.size   = crouchBodySize;
            boxCollider.offset = crouchBodyOffset;
        }
        if(isCrouching && !Input.GetKey(KeyCode.LeftShift) && !isCrouched())
        {
            isCrouching = false;
            anim.SetBool("Crouching", false);
            boxCollider.size = bodySize;
            boxCollider.offset = bodyOffset;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouched())
        {
            anim.SetBool("Crouching", false);
            isCrouching = false;
            boxCollider.size = bodySize;
            boxCollider.offset = bodyOffset;
        }
        if (!Input.GetKeyDown(KeyCode.LeftShift) && !IsOnGround())
        {
            isCrouching = false;
            anim.SetBool("Crouching", false);
            boxCollider.size = bodySize;
            boxCollider.offset = bodyOffset;
        }
    }

    void FlipDirection()
    {
        direction *= -1f;
        transform.Rotate(0f, 180f, 0f);
    }

    void Shoot()
    {
        if (!gotGun)
            return;
        if (Input.GetButtonDown("Fire1")&& !isCrouching && IsOnGround() && !isRunning && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            anim.SetTrigger("Shooting");
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
            
        if (Input.GetButtonDown("Fire1") && isCrouching && IsOnGround() && !isRunning && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            anim.SetTrigger("Shooting");
            Instantiate(bulletPrefab, firePointCrouch.position, firePointCrouch.rotation);
        }
            
    }
    void Climb()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, LayerMask.GetMask("Ladder"));
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, Vector2.down, distance, LayerMask.GetMask("Ladder"));
        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
                rigidBody.position = new Vector2(ladderPosition, rigidBody.position.y);
                anim.SetBool("IsClimbing", true);
            }

        }
        if (hitInfo.collider == null && Input.GetKeyUp(KeyCode.W))
        {
            isClimbing = false;
            anim.SetBool("IsClimbing", false);
        }
        if (hitInfo2.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isClimbing = true;
                rigidBody.position = new Vector2(ladderPosition, rigidBody.position.y);
                anim.SetBool("IsClimbing", true);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                isClimbing = false;
                rigidBody.position = new Vector2(rigidBody.position.x, rigidBody.position.y);
                anim.SetBool("IsClimbing", false);
            }
        }


        else
        {
            isClimbing = false;
            anim.SetBool("IsClimbing", false);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                isClimbing = false;
                rigidBody.position = new Vector2(rigidBody.position.x, rigidBody.position.y);
                anim.SetBool("IsClimbing", false);
            }
        }
        if (isClimbing && hitInfo.collider != null)
        {
            float yVelocity = ladderSpeed * Input.GetAxisRaw("Vertical");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity);

            rigidBody.gravityScale = 0;
        }
        else
            rigidBody.gravityScale = 3;
    }
    void LadderCheck()
    {
        if (transform.position.x > 189f)
        {
            ladderPosition = 202.03f;
            if (transform.position.x >= 215f)
                ladderPosition = 217.93f;
        }
        else
            ladderPosition = 177.46f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathColliders"))
        {
            if (isDead)
                return;
            Die();
            GameManager.PlayerDiedWithoutDelay();
        }
        if (other.CompareTag("Stones"))
        {
            Die();
            GameManager.PlayerDied();
        }
        if (other.CompareTag("Enemy"))
        {
            if (isDead)
                return;
            Die();
            GameManager.PlayerDied();
        }
        if (other.CompareTag("Enemy2"))
        {
            if (isDead)
                return;
            Die();
            GameManager.PlayerDied();
        }
        if (other.CompareTag("GotGun"))
        {
            anim.SetBool("GotGun", true);
            AudioManager.instance.Play("Gun");
            gotGun = true;
            gun.SetActive(false);
            FindObjectOfType<DestroyThis>().DestroyOnTrigger();

        }
        if (other.CompareTag("GotGun2"))
        {
            anim.SetBool("GotGun", true);
            AudioManager.instance.Play("Gun");
            gotGun = true;
            FindObjectOfType<DestroyThis>().DestroyOnTrigger();
            FindObjectOfType<PlayerV2>().ChangeScale();

        }
        if (other.CompareTag("Respawn"))
        {
            anim.SetBool("GotGun", false);
            gotGun = false;
        }
        if (other.CompareTag("Enemy3"))
        {
            if (isDead)
                return;
            Die();
            GameManager.PlayerDied();
        }
        if (other.CompareTag("InactivePlayer"))
        {           
            isPlayerActive = false;
            go = GameObject.FindGameObjectWithTag("InactivePlayer");
            Destroy(go);
        }
        if (other.CompareTag("TubeLight"))
        {
            FindObjectOfType<TubeLight>().TubeLightFall();
        }
    }
    public void Die()
    {
        isDead = true;
        anim.SetBool("Dead",true);
        anim.SetTrigger("IsDead");
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        FindObjectOfType<AudioManager>().Play("PlayerDeathVoice");

    }
    public void FootStep1()
    {
        FindObjectOfType<AudioManager>().Play("FootStep1");
    }
    public void FootStep2()
    {
        FindObjectOfType<AudioManager>().Play("FootStep2");
    }
    public void CFootStep1()
    {
        FindObjectOfType<AudioManager>().Play("CFootStep1");
    }
    public void CFootStep2()
    {
        FindObjectOfType<AudioManager>().Play("CFootStep2");
    }
    public void IsOnPhone()
    {
        anim.SetBool("OnPhone", true);
    }
    public void IsNotOnPhone()
    {
        anim.SetBool("OnPhone", false);
    }

    bool IsOnGround()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, LayerMask.GetMask("Platforms") );
    }

    bool isCrouched()
    {
        return Physics2D.OverlapBox(HeadCheck.position, HeadCheckSize, 0f, LayerMask.GetMask("Platforms"));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawWireCube(HeadCheck.position, HeadCheckSize);
    }




}

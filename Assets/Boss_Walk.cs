using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    public float speed = 4f;
    public float direction = 1f;
    public bool canLazar= false;
    Boss boss;
    Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        FindObjectOfType<Boss>().isMortal = false;
        FindObjectOfType<Boss>().ShowHealthBar();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.transform.position += Vector3.left*direction *speed * Time.deltaTime;
        /*if (FindObjectOfType<Boss>().canShoot)
            FindObjectOfType<Boss>().StartCourotine();*/
        if (FindObjectOfType<Boss>().IsBlocked())
        {
            direction *= -1f;
        }
        /*if (canLazar)
            FindObjectOfType<Boss>().ActualStartShooting();*/
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}

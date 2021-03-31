using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazarBeam : MonoBehaviour
{
    public void LazarAttack()
    {
        GetComponent<Animator>().SetTrigger("LazarBeam");
    }
    public void LazarDone()
    {
        FindObjectOfType<Boss>().LazarOff();
    }
    public void BossLaser()
    {
        FindObjectOfType<AudioManager>().Play("BossLaser");
    }
}

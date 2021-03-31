using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayManager : MonoBehaviour
{
    static DisplayManager current;

    public TextMeshProUGUI orbText;         
    public TextMeshProUGUI deathText;
    private Animator anim;


    void Awake()
    {
       
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        DontDestroyOnLoad(gameObject);
        anim = GetComponent<Animator>();
    }

    public static void UpdateOrbUI(int orbCount)
    {
        if (current == null)
            return;
        current.orbText.text = orbCount.ToString();
    }

    public static void UpdateDeathUI(int deathCount)
    {
        if (current == null)
            return;
        current.deathText.text = deathCount.ToString();
    }
    public static void HideUI()
    {
        current.anim.SetBool("Hide", true);
    }
    public static void UnHideUI()
    {
        current.anim.SetBool("Hide", false);
    }
}

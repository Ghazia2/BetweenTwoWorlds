using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    private float healthBarScale;

    void Start()
    {
        bar = transform.Find("Bar");
        gameObject.SetActive(false);
        
    }
    void Update()
    {
        healthBarScale = FindObjectOfType<Boss>().health / 1520f;
        SetSize();
    }

    public void SetSize()
    {
        bar.localScale = new Vector3(healthBarScale, 1f);
    }
    public void DestroyHealthBar()
    {
        Destroy(gameObject);
    }
    public void SetActiveOn()
    {
        gameObject.SetActive(true);
    }
}

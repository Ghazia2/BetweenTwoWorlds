using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAdjust : MonoBehaviour
{

    public float DeadZoneWidth = 0.5f;
    public float screenY = 0.5f;
    public CinemachineVirtualCamera vcam;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var composer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
            composer.m_DeadZoneWidth = DeadZoneWidth;
            composer.m_ScreenY = screenY;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    public DestroyPlatforms[] destroyablePlatforms;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (DestroyPlatforms desPlatforms in destroyablePlatforms)
            {
                desPlatforms.DestroyPlatformers();
                FindObjectOfType<AudioManager>().Play("Dust");
                Destroy(gameObject);
            }
        }
    }
}

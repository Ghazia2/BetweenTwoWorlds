using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool Dialogue1 = false;
    public bool Dialogue2 = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (Dialogue1)
                FindObjectOfType<DialogueManager>().Dialogue1 = true;
            if (Dialogue2)
                FindObjectOfType<DialogueManager>().Dialogue2 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<PlayerMovement>().isPlayerActive = false;
            Destroy(gameObject);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    private Queue<float> TimeBtwDialogues;
    public bool Dialogue1 = false;
    public bool Dialogue2 = false;
    public Animator animator;

    void Start()
    {
        sentences = new Queue<string>();
        TimeBtwDialogues = new Queue<float>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            EndDialogue();
            return;
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetTrigger("StartBar");
        
        sentences.Clear();
        TimeBtwDialogues.Clear();
        if(Dialogue1)
            Invoke("DialogueSound1", 1f);
        if (Dialogue2)
            Invoke("DialogueSound2", 1f);
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (float Time in dialogue.timeInterval)
        {
            TimeBtwDialogues.Enqueue(Time);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count ==0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        float time = TimeBtwDialogues.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence,time));
    }
    IEnumerator TypeSentence (string sentence , float time)
    {
        dialogueText.text = sentence;
        yield return new WaitForSeconds(time);
        DisplayNextSentence();
    }
    public void EndDialogue()
    {
        animator.SetTrigger("EndBar");
        FindObjectOfType<PlayerMovement>().IsNotOnPhone();
        FindObjectOfType<AudioManager>().Stop("Dialogue1");
        FindObjectOfType<AudioManager>().Stop("Dialogue2");
        FindObjectOfType<PlayerMovement>().isPlayerActive = true;
    }
    public void DialogueSound1()
    {
        FindObjectOfType<AudioManager>().Play("Dialogue1");
        FindObjectOfType<PlayerMovement>().IsOnPhone();
    }
    public void DialogueSound2()
    {
        FindObjectOfType<AudioManager>().Play("Dialogue2");
        FindObjectOfType<PlayerMovement>().IsOnPhone();
    }
}

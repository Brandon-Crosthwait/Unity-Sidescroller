using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}

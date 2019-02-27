using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField]
    private string dialogue;
    [SerializeField]
    private string[] dialogueLines;

    private DialogueManager dialogueManager;
    private bool isActivated;

	// Use this for initialization
	void Start ()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ActivatedDialogue();
	}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //if (collision.gameObject.tag == "Player")
    //    //{
    //    //    if (Input.GetMouseButtonUp(1))
    //    //    {
    //    //        //dialogueManager.ShowBox(dialogue);

    //    //        if (!dialogueManager.MyIsDialogueActive)
    //    //        {
    //    //            dialogueManager.MyDialogLines = dialogueLines;
    //    //            dialogueManager.MyCurrentLine = 0;
    //    //            dialogueManager.ShowDialogue();
    //    //        }
    //    //    }
    //    //}
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isActivated = false;
        }
    }

    private void ActivatedDialogue()
    {
        if (isActivated)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (!dialogueManager.MyIsDialogueActive)
                {
                    dialogueManager.MyDialogLines = dialogueLines;
                    dialogueManager.MyCurrentLine = 0;
                    dialogueManager.ShowDialogue();
                }
            }
        }
    }
}

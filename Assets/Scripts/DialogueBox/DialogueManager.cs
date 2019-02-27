using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    public GameObject dialogueBox;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private bool isDialogueActive;
    [SerializeField]
    private string[] dialogLines;
    [SerializeField]
    private int currentLine;

    public static bool isPaused = false;

    public bool MyIsDialogueActive
    {
        get
        {
            return isDialogueActive;
        }

        set
        {
            isDialogueActive = value;
        }
    }

    public string[] MyDialogLines
    {
        get
        {
            return dialogLines;
        }

        set
        {
            dialogLines = value;
        }
    }

    public int MyCurrentLine
    {
        get
        {
            return currentLine;
        }

        set
        {
            currentLine = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ShowDialogueText();
    }

    public void ShowBox(string dialogue)
    {
        MyIsDialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }

    public void ShowDialogue()
    {
        Time.timeScale = 0f;
        MyIsDialogueActive = true;
        dialogueBox.SetActive(true);
    }

    public void ShowDialogueText()
    {
        if (MyIsDialogueActive && Input.GetKeyUp(KeyCode.Space))
        {
            MyCurrentLine++;
        }

        if (MyCurrentLine >= MyDialogLines.Length)
        {
            Time.timeScale = 1f;
            dialogueBox.SetActive(false);
            MyIsDialogueActive = false;

            MyCurrentLine = 0;
        }

        dialogueText.text = MyDialogLines[MyCurrentLine];
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private QuestObject[] quests;
    private bool[] questCompleted;
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private string killedEnemy;


    //[SerializeField]
    private string itemCollected;


    public QuestObject[] MyQuests
    {
        get
        {
            return quests;
        }

        set
        {
            quests = value;
        }
    }

    public string MyItemCollected
    {
        get
        {
            return itemCollected;
        }

        set
        {
            itemCollected = value;
        }
    }

    public string MyKilledEnemy
    {
        get
        {
            return killedEnemy;
        }

        set
        {
            killedEnemy = value;
        }
    }

    public bool[] MyQuestCompleted
    {
        get
        {
            return questCompleted;
        }

        set
        {
            questCompleted = value;
        }
    }

    void Start ()
    {
        MyQuestCompleted = new bool[MyQuests.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestText(string questText)
    {
        dialogueManager.MyDialogLines = new string[1];
        dialogueManager.MyDialogLines[0] = questText;

        dialogueManager.MyCurrentLine = 0;
        dialogueManager.ShowDialogue();
    }
}

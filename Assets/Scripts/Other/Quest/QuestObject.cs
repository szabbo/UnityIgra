using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    private static QuestObject instance;

    public static QuestObject MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<QuestObject>();

            return instance;
        }
    }


    //basic for all quest
    [SerializeField]
    private int questIndex;
    [SerializeField]
    private QuestManager questManager;
    [SerializeField]
    private string startText;
    [SerializeField]
    private string endText;

    //Quests with items
    [SerializeField]
    private bool isItemQuest;
    private string targetItem;

    //Quests with enemys
    [SerializeField]
    private bool isEnemyQuest;
    [SerializeField]
    private string targetEnemy;
    [SerializeField]
    private int enemysToKill;
    private int numberOfKilledEnemys = 0;

    //neki dodatni uvjeti
    [SerializeField]
    private bool isGhost;
    private bool isCountedIn = true;

    public bool MyIsEnemyQuest
    {
        get
        {
            return isEnemyQuest;
        }

        set
        {
            isEnemyQuest = value;
        }
    }

    public bool MyIsGhost
    {
        get
        {
            return isGhost;
        }

        set
        {
            isGhost = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ItemQuest();
        EnemyQuest();
	}

    public void StartQuest()
    {
        questManager.ShowQuestText(startText);
    }

    public void EndQuest()
    {
        questManager.ShowQuestText(endText);
        questManager.MyQuestCompleted[questIndex] = true;
        gameObject.SetActive(false);
    }

    public void ItemQuest()
    {
        if (isItemQuest)
        {
            if (questManager.MyItemCollected == targetItem)
            {
                questManager.MyItemCollected = null;
                EndQuest();
            }
        }
    }

    public void EnemyQuest()
    {
        if (MyIsEnemyQuest)
        {
            if (questManager.MyKilledEnemy == targetEnemy)
            {
                questManager.MyKilledEnemy = null;
                numberOfKilledEnemys++;

                if (numberOfKilledEnemys == enemysToKill)
                {
                    EndQuest();
                }
            }
        }
    }
}

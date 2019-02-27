using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField]
    private int questIndex;
    [SerializeField]
    private bool startQuest;
    [SerializeField]
    private bool endQuest;

    [SerializeField]
    private QuestObject questObject;
    private bool isActivated = false, myTest;


    private QuestManager questManager;

	// Use this for initialization
	void Start ()
    {
        questManager = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ActivatedQuest();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //go find smthing quests
            if (myTest)
            {
                //if (!questManager.MyQuestCompleted[questIndex])
                //{
                //    if (startQuest && !questManager.MyQuests[questIndex].gameObject.activeSelf)
                //    {
                //        questManager.MyQuests[questIndex].gameObject.SetActive(true);
                //        questManager.MyQuests[questIndex].StartQuest();
                //    }

                //    if (endQuest && questManager.MyQuests[questIndex].gameObject.activeSelf)
                //    {
                //        questManager.MyQuests[questIndex].EndQuest();
                //    }
                //}
            }

            //ghost
            //else if (isActivated && questObject.MyIsGhost)
            //{
            //    if (!questManager.MyQuestCompleted[questIndex])
            //    {
            //        if (startQuest && !questManager.MyQuests[questIndex].gameObject.activeSelf)
            //        {
            //            questManager.MyQuests[questIndex].gameObject.SetActive(true);
            //            questManager.MyQuests[questIndex].StartQuest();
            //        }

            //        if (endQuest && questManager.MyQuests[questIndex].gameObject.activeSelf)
            //        {
            //            if (questIndex != 7)
            //            {
            //                questManager.MyQuests[questIndex].EndQuest();
            //            }

            //            if (questIndex == 7)
            //            {
            //                questManager.MyQuestCompleted[questIndex] = true;
            //                SceneManager.LoadScene("LastScene");
            //            }
            //        }
            //    }
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isActivated = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isActivated = true;
        }
    }

    private void ActivatedQuest()
    {
        if (isActivated && Input.GetMouseButtonDown(1))
        {
            if (!questManager.MyQuestCompleted[questIndex])
            {
                if (startQuest && !questManager.MyQuests[questIndex].gameObject.activeSelf)
                {
                    questManager.MyQuests[questIndex].gameObject.SetActive(true);
                    questManager.MyQuests[questIndex].StartQuest();
                }

                if (endQuest && questManager.MyQuests[questIndex].gameObject.activeSelf)
                {
                    questManager.MyQuests[questIndex].EndQuest();
                }
            }
        }
        else if (isActivated && questObject.MyIsGhost)
        {
            if (!questManager.MyQuestCompleted[questIndex])
            {
                if (startQuest && !questManager.MyQuests[questIndex].gameObject.activeSelf)
                {
                    questManager.MyQuests[questIndex].gameObject.SetActive(true);
                    questManager.MyQuests[questIndex].StartQuest();
                }

                if (endQuest && questManager.MyQuests[questIndex].gameObject.activeSelf)
                {
                    if (questIndex != 7)
                    {
                        questManager.MyQuests[questIndex].EndQuest();
                    }

                    if (questIndex == 7)
                    {
                        questManager.MyQuestCompleted[questIndex] = true;
                        SceneManager.LoadScene("LastScene");
                    }
                }
            }
        }
    }

   
}

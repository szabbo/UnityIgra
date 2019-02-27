using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    //DEBUGGING
    [SerializeField]
    private Questlog tmpLog;

    private void Awake()
    {
        //prihvati quest ovdje
        //DEBUGGING
        tmpLog.AcceptQuest(quests[0]);
        //tmpLog.AcceptQuest(quests[1]);
    }
}

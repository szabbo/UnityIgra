using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questlog : MonoBehaviour
{
    private static Questlog instance;

    public static Questlog MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Questlog>();

            return instance;
        }
    }

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private Quest selected;

    [SerializeField]
    private Text questDescription;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AcceptQuest(Quest quest)
    {
        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;
        //qs.MyQuest.MyQuestScript = qs;

        go.GetComponent<Text>().text = quest.MyTitle;
    }

    public void ShowDescription(Quest quest)
    {
        if (selected != null)
        {
            selected.MyQuestScript.DeSelect();
        }

        string objectives = "\nObjectives:\n";

        selected = quest;
        string title = quest.MyTitle;

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        questDescription.text = string.Format("<b>{0}\n\n<size=10>{1}{2}</size></b>", title, quest.MyDescription, objectives); //<b> - bold
    }
}

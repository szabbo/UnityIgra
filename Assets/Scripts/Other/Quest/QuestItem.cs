using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [SerializeField]
    private int questIndex;
    [SerializeField]
    private string itemName;

    private QuestManager questManager;

	// Use this for initialization
	void Start ()
    {
        questManager = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && Input.GetMouseButtonDown(0))
        {
            if (!questManager.MyQuestCompleted[questIndex] && questManager.MyQuests[questIndex].gameObject.activeSelf)
            {
                questManager.MyItemCollected = itemName;
                gameObject.SetActive(false);
            }
        }
    }
}

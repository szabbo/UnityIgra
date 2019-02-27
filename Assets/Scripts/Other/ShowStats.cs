using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text[] stats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ShowCharStats();
	}

    public void ShowCharStats()
    {
        stats[0].text = string.Format("ARMOR: {0}", player.MyArmor);
        stats[1].text = string.Format("GOLD: {0}", player.MyGold);
    }
}

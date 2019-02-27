using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManaRestore : MonoBehaviour
{

    private bool isActivated;

    private void Update()
    {
        RestoreMana();
        //if (mana && Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log("stisnut button");
        //    Player.MyInstance.MyMana.MyCurrentValue = Player.MyInstance.MyMana.MyMaxValue;
        //}
    }

    private void RestoreMana()
    {
        //Debug.Log("on trigger");
        if(isActivated && Input.GetMouseButtonDown(1))
        {
            //isActivated = true;
            //Debug.Log("unitar ifa");
            Player.MyInstance.MyMana.MyCurrentValue = Player.MyInstance.MyMana.MyMaxValue;
            //Debug.Log("mana: {0}" + Player.MyInstance.MyMana.MyCurrentValue);
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

}

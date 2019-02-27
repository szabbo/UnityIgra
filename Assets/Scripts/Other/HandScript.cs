﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{

    public IMoveable MyMoveable { get; set; }

    private Image icon;

    [SerializeField]
    private Vector3 offset;

    private static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HandScript>();

            return instance;
        }
    }
    // Use this for initialization
    void Start ()
    {
        icon = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        icon.transform.position = Input.mousePosition + offset;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }
	}

    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);

        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        InventoryScript.MyInstance.FromSlot = null;
    }

    public void DeleteItem()
    {
        if (MyMoveable is Item)
        {
            Item item = (Item)MyMoveable;

            if (item.MySlot != null)
            {
                item.MySlot.Clear();
            }
            else if (item.MyCharButton != null)
            {
                item.MyCharButton.DequipArmor();
            }
        }

        Drop();

        InventoryScript.MyInstance.FromSlot = null;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

    public BagScript MyBag { get; set; }

    public bool IsEmpty { get { return MyItems.Count == 0; } }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
                return false;

            return true;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
            }

            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount { get { return MyItems.Count; } }

    public Text MyStackText { get { return stackSize; } }

    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }

    private void Awake()
    {
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool AddItem(Item item)
    {
        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;

        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }

            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }

    public void Clear()
    {
        if (MyItems.Count > 0)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            MyItems.Clear();
        }
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Left)
    //    {
    //        if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty) //ako se nista ne treba micati
    //        {
    //            if (HandScript.MyInstance.MyMoveable != null)
    //            {
    //                if (HandScript.MyInstance.MyMoveable is Bag)
    //                {
    //                    if (MyItem is Bag)
    //                    {
    //                        InventoryScript.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
    //                    }
    //                }
    //                else if (HandScript.MyInstance.MyMoveable is Armor)
    //                {
    //                    if (MyItem is Armor && (MyItem as Armor).MyArmorType == (HandScript.MyInstance.MyMoveable as Armor).MyArmorType)
    //                    {
    //                        (MyItem as Armor).Equip();
    //                        //UIManager.MyInstance.RefreshTooltip();
    //                        HandScript.MyInstance.Drop();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
    //                InventoryScript.MyInstance.FromSlot = this;
    //            }
    //        }
    //        else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
    //        {
    //            if (HandScript.MyInstance.MyMoveable is Bag)
    //            {
    //                //unequip itema
    //                Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

    //                //brine se da kada skinemo torbu imamo dovoljno mijesta za iteme koji su u skinutoj torbi
    //                if (bag.MyBagScript != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - bag.Slots > 0)
    //                {
    //                    AddItem(bag);
    //                    bag.MyBagButton.RemoveBag();
    //                    HandScript.MyInstance.Drop();
    //                }
    //            }
    //            else if (HandScript.MyInstance.MyMoveable is Armor)
    //            {
    //                Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
    //                CharacterPanel.MyInstance.MySelectedButton.DequipArmor();
    //                AddItem(armor);
    //                HandScript.MyInstance.Drop();

    //            }
    //        }
    //        else if (InventoryScript.MyInstance.FromSlot != null) //ak se nesto treba pomaknuti
    //        {
    //            //vracanje itema u inventory
    //            if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) || SwapItems(InventoryScript.MyInstance.FromSlot) || AddItems(InventoryScript.MyInstance.FromSlot.MyItems))
    //            {
    //                HandScript.MyInstance.Drop();
    //                InventoryScript.MyInstance.FromSlot = null;
    //            }
    //        }
    //    }
    //    if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null)
    //    {
    //        UseItem();
    //    }
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty) //ako se nista ne treba micati
            {
                if (HandScript.MyInstance.MyMoveable != null)
                {
                    if (HandScript.MyInstance.MyMoveable is Bag)
                    {
                        if (MyItem is Bag)
                        {
                            InventoryScript.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        }
                    }
                    else if (HandScript.MyInstance.MyMoveable is Armor)
                    {
                        if (MyItem is Armor && (MyItem as Armor).MyArmorType == (HandScript.MyInstance.MyMoveable as Armor).MyArmorType)
                        {
                            (MyItem as Armor).Equip();
                            //UIManager.MyInstance.RefreshTooltip();
                            HandScript.MyInstance.Drop();
                        }
                    }
                }
                else
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.MyInstance.FromSlot = this;
                }
            }
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
            {
                if (HandScript.MyInstance.MyMoveable is Bag)
                {
                    //unequip itema
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                    //brine se da kada skinemo torbu imamo dovoljno mijesta za iteme koji su u skinutoj torbi
                    if (bag.MyBagScript != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - bag.Slots > 0)
                    {
                        AddItem(bag);
                        bag.MyBagButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                    }
                }
                else if (HandScript.MyInstance.MyMoveable is Armor)
                {
                    Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
                    //AddItem(armor);
                    CharacterPanel.MyInstance.MySelectedButton.DequipArmor();
                    AddItem(armor);
                    HandScript.MyInstance.Drop();
                    //AddItem(armor);
                }
            }
            else if (InventoryScript.MyInstance.FromSlot != null) //ak se nesto treba pomaknuti
            {
                //vracanje itema u inventory
                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) || SwapItems(InventoryScript.MyInstance.FromSlot) || AddItems(InventoryScript.MyInstance.FromSlot.MyItems))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
        else if (MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
        {
            MyItems.Push(item);
            item.MySlot = this;

            return true;
        }

        return false;
    }

    private bool PutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;

            return true;
        }

        return false;
    }

    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
        {
            //kopija itema iz slota A
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);

            //ocisti slot A
            from.MyItems.Clear();
            //uzmemo sve iteme iz slota B i kopira ih u slot A
            from.AddItems(MyItems);

            //ocisti slot B
            MyItems.Clear();
            //premjesti iteme iz Atmp u B
            AddItems(tmpFrom);

            return true;
        }

        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            //broj praznih slotova
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.MyItems.Pop());
            }

            return true;
        }

        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    //prikazuje tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(1, 0), transform.position, MyItem);
        }
    }

    //skriva tooltip
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    public IUseable MyUseable { get; set; }
    private Stack<IUseable> useables = new Stack<IUseable>();
    private int count;
    public Button MyButton { get; private set; }

    [SerializeField]
    private Text stackSize;

    [SerializeField]
    private Image icon;

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

    public int MyCount
    {
        get
        {
            return count;
        }
    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public Stack<IUseable> MyUseables
    {
        get
        {
            return useables;
        }

        set
        {
            MyUseable = value.Peek();
            useables = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            if (MyUseables != null && MyUseables.Count > 0)
            {
                MyUseables.Peek().Use();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
            }

        }
    }

    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            MyUseables = InventoryScript.MyInstance.GetUseables(useable);
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
        }
        else
        {
            MyUseables.Clear();
            this.MyUseable = useable;
        }

        count = MyUseables.Count;
        UpdateVisual();
        UIManager.MyInstance.RefreshTooltip(MyUseable as IDescibable);
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;

        if (count > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
        else if (MyUseable is Spell)
        {
            UIManager.MyInstance.ClearStackCount(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUseable && MyUseables.Count > 0)
        {
            if (MyUseables.Peek().GetType() == item.GetType())
            {
                MyUseables = InventoryScript.MyInstance.GetUseables(item as IUseable);
                count = MyUseables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IDescibable tmp = null;
        if (MyUseable != null && MyUseable is IDescibable)
        {
            tmp = (IDescibable)MyUseable;
            //UIManager.MyInstance.ShowTooltip(transform.position);
        }
        else if (MyUseables.Count > 0)
        {
            //UIManager.MyInstance.ShowTooltip(transform.position);
        }
        if (tmp != null)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(1, 0), transform.position, tmp);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}

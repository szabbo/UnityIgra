using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    private static LootWindow instance;

    public static LootWindow MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<LootWindow>();

            return instance;
        }
    }

    [SerializeField]
    private LootButton[] lootButtons;

    private CanvasGroup canvasGroup;

    private List<List<Item>> pages = new List<List<Item>>();
    private List<Item> droppedLoot = new List<Item>();

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private GameObject previousBtn, nextBtn;

    public bool IsOpen { get { return canvasGroup.alpha > 0; } }

    //debuganje
    [SerializeField]
    private Item[] items;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void CreatePages(List<Item> items)
    {
        if (!IsOpen)
        {
            List<Item> page = new List<Item>();
            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 4 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Item>();
                }
            }

            AddLoot();
            Open();
        }
    }
	
	private void AddLoot()
    {
        if (pages.Count > 0)
        {
            //podesavanje brojeva stranica
            pageNumber.text = pageIndex + 1 + "/" + pages.Count;

            //podesavanje previous i next buttona
            previousBtn.SetActive(pageIndex > 0);
            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);

            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex] != null)
                {
                    //postavlja ikonu
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;
                    lootButtons[i].MyLoot = pages[pageIndex][i];

                    //postavlja buttone vidljivima
                    lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);

                    //postavlja titlea
                    lootButtons[i].MyTitle.text = title;
                }
            }
        }
    }

    public void ClearButtons()
    {
        foreach (LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        //provjera jesmo li na zadnjoj stranici
        if (pageIndex < pages.Count - 1)
        {
            pageIndex++;
            ClearButtons();
            AddLoot();
        }
    }

    public void PreviousPage()
    {
        //provjera imamo li vise od jedne stranice
        if (pageIndex > 0)
        {
            pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);
        droppedLoot.Remove(loot);

        if (pages[pageIndex].Count == 0)
        {
            //micanje prazne stranice
            pages.Remove(pages[pageIndex]);

            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }

            AddLoot();
        }
    }

    public void Close()
    {
        pages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButtons();
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}

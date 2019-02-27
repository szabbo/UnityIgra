using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    private static CharacterSelect instance;

    public static CharacterSelect MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CharacterSelect>();

            return instance;
        }
    }

    private GameObject[] characterList;
    private int index;

    public int CharSelectIndex
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    private void Start()
    {
        CharSelectIndex = PlayerPrefs.GetInt("Select", CharSelectIndex);
        characterList = new GameObject[transform.childCount];

        //ucitmo sve likove
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        //ugase se rendereri
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[CharSelectIndex])
        {
            characterList[CharSelectIndex].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        characterList[CharSelectIndex].SetActive(false);
        CharSelectIndex--;

        if (CharSelectIndex < 0)
        {
            CharSelectIndex = characterList.Length - 1;
        }

        characterList[CharSelectIndex].SetActive(true);
    }

    public void ToggleRight()
    {
        characterList[CharSelectIndex].SetActive(false);
        CharSelectIndex++;

        if (CharSelectIndex > characterList.Length - 1)
        {
            CharSelectIndex = 0;
        }

        characterList[CharSelectIndex].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("Select", CharSelectIndex);

        if (CharSelectIndex == 0)
        {
            SceneManager.LoadScene("Scene01");
        }
        else if (CharSelectIndex == 1)
        {
            SceneManager.LoadScene("Scene02");
        }
        else if (CharSelectIndex == 2)
        {
            SceneManager.LoadScene("Scene03");
        }
    }
}

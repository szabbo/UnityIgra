using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characterList;
    private int characterIndex;

	// Use this for initialization
	void Start ()
    {
        characterIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Next()
    {
        characterIndex++;
        characterList[characterIndex].SetActive(true);
        characterList[characterIndex - 1].SetActive(false);

        if (characterIndex > characterList.Length)
        {
            characterIndex = 0;
        }
    }

    public void Previous()
    {
        characterIndex--;
        characterList[characterIndex].SetActive(true);
        characterList[characterIndex + 1].SetActive(false);

        if (characterIndex < 0)
        {
            characterIndex = characterList.Length;
        }
    }
}

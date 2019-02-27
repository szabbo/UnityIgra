using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform map;

    [SerializeField]
    private Texture2D[] mapData;

    [SerializeField]
    private MapElement[] mapElements;

    [SerializeField]
    private Sprite defaultTile;

    private Vector3 WorldStartPosition
    {
        get { return Camera.main.ScreenToWorldPoint(new Vector3(0, 0)); }
    }

	// Use this for initialization
	void Start ()
    {
        GenerateMap();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void GenerateMap()
    {
        for (int i = 0; i < mapData.Length; i++)
        {
            for (int x = 0; x < mapData[i].width; x++)
            {
                for (int y = 0; y < mapData[i].height; y++)
                {
                    Color someColor = mapData[i].GetPixel(x, y);
                    MapElement newElement = Array.Find(mapElements, someElement => someElement.MyColor == someColor);

                    if (newElement != null)
                    {
                        //GameObject go = Instantiate(newElement.MyElementPrefab);
                        //go.transform.position = WorldStartPosition;
                        float xPosition = WorldStartPosition.x + (defaultTile.bounds.size.x * x);
                        float yPosition = WorldStartPosition.y + (defaultTile.bounds.size.y * y);

                        GameObject gameO = Instantiate(newElement.MyElementPrefab);
                        gameO.transform.position = new Vector2(xPosition, yPosition);
                        gameO.transform.parent = map;
                    }
                }
            }
        }
    }
}

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;
    [SerializeField]
    private Color color;
    [SerializeField]
    private GameObject elementPrefab;

    public GameObject MyElementPrefab
    {
        get { return elementPrefab;}
    }

    public Color MyColor
    {
        get { return color; }
    }

    public string MyTileTag
    {
        get { return tileTag; }
    }
}

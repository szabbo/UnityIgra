using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Spell : IUseable, IMoveable, IDescibable
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int damage;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float castTime;
    [SerializeField]
    private GameObject spellPrefab;
    [SerializeField]
    private string description;
    [SerializeField]
    private Color castBarColor;

    public string MyName
    {
        get { return name; }
        set { name = value; }
    }

    public int MyDamage
    {
        get { return damage; }
        set { damage = value; }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public float MySpeed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float MyCastTime
    {
        get { return castTime; }
        set { castTime = value; }
    }

    public GameObject MySpellPrefab
    {
        get { return spellPrefab; }
        set { spellPrefab = value; }
    }

    public Color MyCastBarColor
    {
        get { return castBarColor; }
        set { castBarColor = value; }
    }

    public string GetDescription()
    {
        return string.Format("{0}\n\nCast time: {1}s\n<color=#ffd111>{2}\nthat causes {3} damage.</color>", name, castTime, description, damage);
    }

    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }
}

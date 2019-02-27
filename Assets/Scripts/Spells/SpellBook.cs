using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{

    [SerializeField]
    private Image castingBarFill;

    [SerializeField]
    private Image spellIcon;

    [SerializeField]
    private Text currentSpell;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Spell[] spells;

    private Coroutine spellRoutine;
    private Coroutine barFadeRoutine;

    private static SpellBook instance;

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }

            return instance;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Spell CastSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        castingBarFill.fillAmount = 0.0f;
        castingBarFill.color = spell.MyCastBarColor;
        currentSpell.text = spell.MyName;
        spellIcon.sprite = spell.MyIcon;

        spellRoutine = StartCoroutine(Progress(spell));
        barFadeRoutine = StartCoroutine(BarFade());

        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float passedTime = Time.deltaTime;
        float rate = 1.0f / spell.MyCastTime;
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            castingBarFill.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            passedTime += Time.deltaTime;
            castTime.text = (spell.MyCastTime - passedTime).ToString("F1");

            if (spell.MyCastTime - passedTime < 0)
            {
                castTime.text = "0.0";
            }

            yield return null;
        }

        StopCasting();
    }

    private IEnumerator BarFade()
    {
        float timeLeft = Time.deltaTime;
        float rate = 1.0f / 0.25f;
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }
    }

    public void StopCasting()
    {
        if (barFadeRoutine != null)
        {
            StopCoroutine(BarFade());
            canvasGroup.alpha = 0;
            barFadeRoutine = null;
        }
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }

    public Spell GetSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        return spell;
    }
}

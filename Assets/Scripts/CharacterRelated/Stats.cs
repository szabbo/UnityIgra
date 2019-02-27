using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private Image content;
    private float currentFill;
    public float MyMaxValue { get; set; }
    private float currentValue;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Text statsText;

    public float MyCurrentValue
    {
        get { return currentValue; }
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;

            if (statsText != null)
                statsText.text = currentValue + " / " + MyMaxValue;
        }
    }

    // Use this for initialization
    void Start ()
    {
        content = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentFill != content.fillAmount)
        {
            //content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, lerpSpeed * Time.deltaTime);
            //bez lerpa
            content.fillAmount = currentFill;
        }
	}

    public void Initialize (float currentValue, float maxValue)
    {
        if (content == null)
        {
            content = GetComponent<Image>();
        }

        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
        content.fillAmount = MyCurrentValue / MyMaxValue;
    }
}

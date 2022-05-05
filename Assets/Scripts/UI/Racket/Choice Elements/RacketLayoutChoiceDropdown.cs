using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceDropdown : RacketLayoutChoiceElement
{
    private TMP_Dropdown _Dropdown;


    protected override void Initialize()
    {
        _Dropdown = GetComponent<TMP_Dropdown>();
        _Dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(int arg0)
    {
        // Set question answered
        if (setAnswered)
            _Question.answered = true;
    }

    [ContextMenu("Change Font Size")]
    void ChangeFontSize()
    {

        var rect = GetComponent<RectTransform>().rect;
        rect.height = 35;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);

        transform.GetChild(0).GetComponent<Image>().pixelsPerUnitMultiplier = 30;


        transform.GetChild(1).GetComponent<TextMeshProUGUI>().enableAutoSizing = false;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 15;
    }
}
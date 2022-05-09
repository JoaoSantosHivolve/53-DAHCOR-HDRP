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

        transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 35f * _Dropdown.options.Count);
        var item = _Dropdown.itemText.transform.parent;
        item.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 35f);
        item.GetChild(2).GetComponent<RectTransform>().offsetMin = new Vector2(25, 0); // new Vector2(left, bottom)
        item.GetChild(2).GetComponent<RectTransform>().offsetMin = new Vector2(25, 0); // new Vector2(left, bottom).offsetMax = new Vector2(25, 0); // new Vector2(right, top)
        item.GetChild(2).GetComponent<TextMeshProUGUI>().enableAutoSizing = false;
        item.GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = 15;
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

    public override void UpdateData()
    {
    }
}
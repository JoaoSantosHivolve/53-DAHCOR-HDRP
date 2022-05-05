using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceButton : RacketLayoutChoiceElement
{
    private Color _SelectedColor = Color.black;
    private Color _UnselectedColor = new Color(213f/255f, 213f/255f, 213f/255f);
    private Button _Button;
    private Image _Image;
        

    protected override void Initialize()
    {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OnClick);
        _Image = GetComponent<Image>();
        _Question.AddButton(this);
    }



    private void OnClick()
    {
        // Set question answered
        if(setAnswered)
            _Question.answered = true;

        // Change Button Outline Color
        _Image.color = _SelectedColor;

        // Set other buttons Unselected
        _Question.ClearOtherSelectedButtons(this);

        // Send condition if any
        _Question.OnChoiceClick(condition);
    }

    public void SetUnselected()
    {
        _Image.color = _UnselectedColor;
    }


    [ContextMenu("Change Font Size")]
    void ChangeFontSize()
    {
        var rect = GetComponent<RectTransform>().rect;
        rect.height = 25;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);

        GetComponent<Image>().pixelsPerUnitMultiplier = 30;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().enableAutoSizing = false;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 15;
    }
}
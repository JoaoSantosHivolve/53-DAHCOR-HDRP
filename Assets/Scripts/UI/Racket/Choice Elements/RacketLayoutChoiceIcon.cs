using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceIcon : RacketLayoutChoiceElement
{
    private Button _Button;
    private GameObject _Outline;

    protected override void Initialize()
    {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OnClick);

        _Outline = transform.GetChild(1).gameObject;

        _Question.AddIcon(this);

        SetUnselected();
    }

    private void OnClick()
    {
        // Set question answered
        if (setAnswered)
            _Question.answered = true;

        // Set outline
        _Outline.SetActive(true);

        // Set other buttons Unselected
        _Question.ClearOtherSelectedIcons(this);

        // Send condition if any
        _Question.OnChoiceClick(condition);
    }

    public void SetUnselected()
    {
        _Outline.SetActive(false);
    }
}
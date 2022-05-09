using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceIcon : RacketLayoutChoiceElement
{
    private RacketLayoutQuestionIcon _IconTypeQuestion;
    private Button _Button;
    private GameObject _Outline;

    protected override void Initialize()
    {
        _IconTypeQuestion = (RacketLayoutQuestionIcon)_Question;

        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OnClick);

        _Outline = transform.GetChild(1).gameObject;

        _Question.AddChoiceElement(this);

        SetUnselected();
    }

    private void OnClick()
    {
        // Set question answered
        if (_SetAnswered)
            _Question.SetAnswered();

        // Set outline
        _Outline.SetActive(true);

        // Set other buttons Unselected
        _IconTypeQuestion.ClearOtherSelectedIcons(this);

        // Send condition if any
        _IconTypeQuestion.OnAnsweringQuestion();
    }

    public void SetUnselected()
    {
        _Outline.SetActive(false);
    }


    [ContextMenu("Change Font Size")]
    void ChangeFontSize()
    {
        var y = transform.GetChild(0).GetComponent<RectTransform>().rect.height * 1f;
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector3(y, 0);
        y = transform.GetChild(1).GetComponent<RectTransform>().rect.height * 1f;
        transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector3(y, 0);

        transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = 12.5f;
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontSize = 10;
    }

    public override void UpdateData()
    {
    }
}
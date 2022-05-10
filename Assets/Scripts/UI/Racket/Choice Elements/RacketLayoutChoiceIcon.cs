using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceIcon : RacketLayoutChoiceElement
{
    private RacketLayoutQuestionIcon _IconTypeQuestion;
    private Button _Button;

    private Image _Image;
    private GameObject _Outline;
    private TextMeshProUGUI _Name;
    private TextMeshProUGUI _Price;

    protected override void Initialize()
    {
        _IconTypeQuestion = (RacketLayoutQuestionIcon)_Question;

        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OnClick);

        _Image = transform.GetChild(0).GetComponent<Image>();
        _Outline = transform.GetChild(1).gameObject;
        _Name = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _Price = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

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
        (_Question as RacketLayoutQuestionIcon).ClearOtherSelectedIcons(this);

        // Send condition if any
        (_Question as RacketLayoutQuestionIcon).OnAnsweringQuestion();
    }

    public void SetUnselected()
    {
        _Outline.SetActive(false);
    }

    public void SetData(Sprite image, string name, string price)
    {
        _Image.sprite = image;
        _Name.text = name;
        _Price.text = price;
    }
    public void SetData(Sprite image, string name, string color, string price)
    {
        SetData(image, name, price);

        var r = color[0] + color[1] + color[2];
        var g = color[3] + color[4] + color[5];
        var b = color[6] + color[7] + color[8];
        var newColor = new Color(r, g, b);

        _Image.color = newColor;
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
}
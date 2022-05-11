using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceIcon : RacketLayoutChoiceElement
{
    private Button _Button;
    private Image _Image;
    private Mask _ImageMask;
    private GameObject _Outline;
    private TextMeshProUGUI _Name;
    private TextMeshProUGUI _Price;

    protected override void Initialize()
    {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OnClick);

        _ImageMask = transform.GetChild(0).GetComponent<Mask>();
        _Image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _Outline = transform.GetChild(1).gameObject;
        _Name = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _Price = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
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

    public IEnumerator UpdateComponents()
    {
        var height = _ImageMask.GetComponent<RectTransform>().rect.height;
        _ImageMask.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);
        _Outline.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);

        yield return new WaitForEndOfFrame(); // The second one is to prevent the 0 width bug

        height = _ImageMask.GetComponent<RectTransform>().rect.height;
        _ImageMask.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);
        _Outline.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);

    }
    public void SetUnselected()
    {
        _Outline.SetActive(false);
    }
    public void SetData(Sprite image, string name, string price)
    {
        _Image.sprite = image;
        _Name.text = name;
        _Price.text = "+" + price;

        SetUnselected();
    }
    public void SetData(Sprite image, string name, string color, string price)
    {
        SetData(image, name, price);

        var r = color[0] + color[1] + color[2];
        var g = color[3] + color[4] + color[5];
        var b = color[6] + color[7] + color[8];
        var newColor = new Color((int)r / 255f, (int)g / 255f, (int)b / 255f);

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
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ChoiceIconType
{
    NotSet,
    Color,
    Texture
}

public class RacketLayoutChoiceIcon : RacketLayoutChoiceElement
{
    private Button _Button;
    private Image _Image;
    private Mask _ImageMask;
    private GameObject _Outline;
    private GameObject _SecondOutline;
    private TextMeshProUGUI _Name;
    private TextMeshProUGUI _Price;
    private ChoiceIconType _Type = ChoiceIconType.NotSet;
    private PartToModify _PartToModify = PartToModify.None;
    // Data
    private Color _DataColor;
    private Texture2D _DataTexture;

    protected override void Initialize()
    {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OnClick);

        _ImageMask = transform.GetChild(0).GetComponent<Mask>();
        _Image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _Outline = transform.GetChild(1).gameObject;
        _Name = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _Price = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        _SecondOutline = transform.GetChild(4).gameObject;
    }
    private void OnClick()
    {
        // Set question answered
        if (_SetAnswered)
            _Question.SetAnswered();

        // Change racket model
        switch (_Type)
        {
            case ChoiceIconType.Color:
                RacketMaterialController.Instance.ChangePart(_PartToModify, _DataColor);
                break;
            case ChoiceIconType.Texture:
                RacketMaterialController.Instance.ChangePart(_PartToModify, _DataTexture);
                break;
            default:
                break;
        }

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
        _SecondOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);

        yield return new WaitForEndOfFrame(); // The second one is to prevent the 0 width bug

        height = _ImageMask.GetComponent<RectTransform>().rect.height;
        _ImageMask.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);
        _Outline.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);
        _SecondOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(height, 0);
    }
    public void SetUnselected()
    {
        _Outline.SetActive(false);
    }
    public void SetData(PartToModify partToModify, Sprite image, string name, string price)
    {
        SetComponents(image, name, price);

        SetUnselected();

        _PartToModify = partToModify;
        _Type = ChoiceIconType.Texture;
        _DataTexture = image.texture;
    }

    public void SetData(PartToModify partToModify, Sprite image, string name, string color, string price)
    {
        SetComponents(image, name, price);

        var r = int.Parse(color.Substring(0, 3));
        var g = int.Parse(color.Substring(3, 3));
        var b = int.Parse(color.Substring(6, 3));
        var newColor = new Color(r / 255f, g / 255f, b / 255f);
        _Image.color = newColor;

        SetUnselected();

        _PartToModify = partToModify;
        _Type = ChoiceIconType.Color;
        _DataColor = newColor;
    }

    private void SetComponents(Sprite image, string name, string price)
    {
        _Image.sprite = image;
        _Name.text = name;
        _Price.text = "+" + price;
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
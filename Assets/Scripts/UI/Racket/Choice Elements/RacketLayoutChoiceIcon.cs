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
    private Transform _OverlaysHolder;
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
        _OverlaysHolder = transform.GetChild(5);

        ClearFinishOverlay();
    }
    private void OnClick()
    {
        // Set question answered
        _Question.SetAnswered();

        // Set outline
        _Outline.SetActive(true);

        // Set other buttons Unselected
        (_Question as RacketLayoutQuestionIcon).ClearOtherSelectedIcons(this);

        // Change racket model
        switch (_Type)
        {
            case ChoiceIconType.Color:
                RacketCostumizerController.Instance.ChangePart(_PartToModify, _DataColor);
                break;
            case ChoiceIconType.Texture:
                RacketCostumizerController.Instance.ChangePart(_PartToModify, _DataTexture);
                break;
            default:
                break;
        }
    }

    public void SetComponentsSize(float value)
    {
        var multiplier = _ImageMask.rectTransform.anchorMax.y - _ImageMask.rectTransform.anchorMin.y;
        var newWidth = value * multiplier;
        _ImageMask.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, 0);
        _Outline.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, 0);
        _SecondOutline.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, 0);
        _OverlaysHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, 0);
    }

    public void SetUnselected()
    {
        _Outline.SetActive(false);
    }

    public void SetData(PartToModify partToModify, Color color)
    {
        SetComponentsData("", "", color);

        SetUnselected();

        _PartToModify = partToModify;
        _Type = ChoiceIconType.Color;
    }
    public void SetData(PartToModify partToModify, string name, string price, Sprite image)
    {
        SetComponentsData(name, price, image);
        SetUnselected();

        _PartToModify = partToModify;
        _Type = ChoiceIconType.Texture;
    }

    private void SetComponentsData(string name, string price)
    {
        _Name.text = name;
        _Price.text = price != "" ? "+" + price : "";
    }
    private void SetComponentsData(string name, string price, Sprite image)
    {
        SetComponentsData(name, price);

        _Image.sprite = image == null ? _Image.sprite : image;
        _DataTexture = image.texture;
    }
    private void SetComponentsData(string name, string price, Color color)
    {
        SetComponentsData(name, price);

        _Image.color = color;
        _DataColor = color;

      

        //var r = int.Parse(color.Substring(0, 3));
        //var g = int.Parse(color.Substring(3, 3));
        //var b = int.Parse(color.Substring(6, 3));
        //var newColor = new Color(r / 255f, g / 255f, b / 255f);
    }

    public void AddFinishOverlay(PremadeFinish finish)
    {
        ClearFinishOverlay();

        _OverlaysHolder.GetChild((int)finish).gameObject.SetActive(true);
    }
    private void ClearFinishOverlay()
    {
        for (int i = 0; i < _OverlaysHolder.childCount; i++)
        {
            _OverlaysHolder.GetChild(i).gameObject.SetActive(false);
        }
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
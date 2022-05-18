using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataTypeToLoad
{
    NotSetYet,
    Colors,
    Skins,
    Element,
    Precious
}

public class RacketLayoutQuestionIcon : RacketLayoutQuestion
{
    [SerializeField] private DataTypeToLoad _DataTypeToLoad = DataTypeToLoad.NotSetYet;
    [SerializeField] private PartToModify _PartToModify = PartToModify.None;

    private GridLayoutGroup _GridLayoutGroup;
    [SerializeField] private List<RacketLayoutChoiceIcon> _Cells;

    private void OnEnable()
    {
       if (_GridLayoutGroup == null)
           return;
       
       RefreshUi();
       
       var cellWidth = transform.GetComponent<RectTransform>().rect.width / 4f;
       _GridLayoutGroup.cellSize = new Vector2(cellWidth, cellWidth);
       _GridLayoutGroup.spacing = new Vector2(0, 0);
       
       RefreshUi();
       
       foreach (var item in _Cells)
       {
           StartCoroutine(item.UpdateComponents());
       }
    }

    public override void Initialize()
    {
        _GridLayoutGroup = transform.Find("Grid").GetComponent<GridLayoutGroup>();
        _Cells = new List<RacketLayoutChoiceIcon>();
    }
    public override void OnReset()
    {
        if (_Cells.Count == 0)
            return;

        foreach (RacketLayoutChoiceIcon item in _Cells)
        {
            item.SetUnselected();
        }
    }
    public override void UpdateData()
    {
        // clear grid before populating with new data
        ClearGrid();

        // Populate grid
        var prefab = Resources.Load<RacketLayoutChoiceIcon>("Prefabs/Ui/IconDisplay");

        if (_DataTypeToLoad == DataTypeToLoad.NotSetYet)
        {
            return;
        }
        else
        {
            // Colors
            if (_DataTypeToLoad == DataTypeToLoad.Colors)
            {
                var colorData = DataLoader.Instance.GetColorData();
                var circleSprite = Resources.Load<Sprite>("Images/Circle");

                for (int i = 0; i < colorData.Count; i++)
                {
                    var currentData = colorData[i];
                    var cell = Instantiate(prefab.gameObject, _GridLayoutGroup.transform).GetComponent<RacketLayoutChoiceIcon>();
                    cell.InitializeChoiceElement(this);
                    cell.SetData(_PartToModify, circleSprite, currentData.name, currentData.color, currentData.price);

                    _Cells.Add(cell);
                }
            }
            // Skins // Element // Precious
            else
            {
                // Get data
                var data = GetCurrentTextureData();

                for (int i = 0; i < data.Count; i++)
                {
                    var currentData = data[i];

                    var textureString = currentData.texture;
                    var textureData = System.Convert.FromBase64String(textureString);
                    var texture = new Texture2D(128, 128);
                    texture.LoadImage(textureData);
                    var skinTextureSprite = Sprite.Create(texture,
                            new Rect(0.0f, 0.0f,texture.width, texture.height),
                            new Vector2(0.5f, 0.5f), 100.0f);

                    var cell = Instantiate(prefab.gameObject, _GridLayoutGroup.transform).GetComponent<RacketLayoutChoiceIcon>();
                    cell.InitializeChoiceElement(this);
                    cell.SetData(_PartToModify, skinTextureSprite, currentData.name, currentData.price);

                    _Cells.Add(cell);
                }
            }
        }
    }

    public void ClearOtherSelectedIcons(RacketLayoutChoiceIcon icon)
    {
        foreach (RacketLayoutChoiceIcon item in _Cells)
        {
            if (item != icon)
                item.SetUnselected();
        }
    }

    private List<TextureData> GetCurrentTextureData()
    {
        switch (_DataTypeToLoad)
        {
            case DataTypeToLoad.Skins:
                return DataLoader.Instance.GetSkinData();
            case DataTypeToLoad.Element:
                return DataLoader.Instance.GetElementData();
            case DataTypeToLoad.Precious:
                return DataLoader.Instance.GetPreciousData();
            default:
                return null;
        }

    }
    private void ClearGrid()
    {
        for (int i = 0; i < _GridLayoutGroup.transform.childCount; i++)
        {
            Destroy(_GridLayoutGroup.transform.GetChild(i).gameObject);
        }

        // reset list
        _Cells = new List<RacketLayoutChoiceIcon>();
    }
}
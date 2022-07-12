using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataTypeToLoad
{
    NotSetYet,
    Colors,
    Woods, 
    Rocks,
    Scifi,
    Fabrics,
    Organic,
    Military,
    Animals,
    Precious
}

public class RacketLayoutQuestionIcon : RacketLayoutQuestion
{
    [SerializeField] private DataTypeToLoad _DataTypeToLoad = DataTypeToLoad.NotSetYet;
    [SerializeField] private PartToModify _PartToModify = PartToModify.None;
    [SerializeField] private int _PartIndex = 0;

    private GridLayoutGroup _GridLayoutGroup;
    private List<RacketLayoutChoiceIcon> _Cells;

    private void OnEnable()
    {
        RefreshUi(); // Fixes visual bugs
    }

    public override void Initialize()
    {
        _GridLayoutGroup = transform.Find("Grid").GetComponent<GridLayoutGroup>();
        _Cells = new List<RacketLayoutChoiceIcon>();

        //var cellWidth = transform.GetComponent<RectTransform>().rect.width / 4f;
        var cellWidth = 472.1125f / 4f; // 472.1125f is the with of the panel, when the opening animation ends
        _GridLayoutGroup.cellSize = new Vector2(cellWidth, cellWidth);
        _GridLayoutGroup.spacing = new Vector2(0, 0);
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
        InstantiateGrid(_DataTypeToLoad);
    }

    public void InstantiateGrid(int index)
    {
        InstantiateGrid((DataTypeToLoad)index);
    }
    private void InstantiateGrid(DataTypeToLoad dataToLoad)
    {
        _DataTypeToLoad = dataToLoad;

        // clear grid before populating with new data
        ClearGrid();

        // Leave function if no data to load
        if (_DataTypeToLoad == DataTypeToLoad.NotSetYet)
            return;

        // Get icon prefab
        var prefab = Resources.Load<RacketLayoutChoiceIcon>("Prefabs/Ui/IconDisplay");
        var parent = _GridLayoutGroup.transform;

        // Colors
        if (_DataTypeToLoad == DataTypeToLoad.Colors)
        {
            var colorData = DataLoader.Instance.GetColorData();

            for (int i = 0; i < colorData.Count; i++)
            {
                var currentColorData = colorData[i];
                var cell = Instantiate(prefab.gameObject, parent).GetComponent<RacketLayoutChoiceIcon>();
                cell.InitializeChoiceElement(this);
                cell.SetData(_PartIndex, _PartToModify, currentColorData.color);
                cell.SetComponentsSize(_GridLayoutGroup.cellSize.y);

                _Cells.Add(cell);
            }
        }
        // Immaterials
        else
        {
            var textureData = GetCurrentTextureData();

            for (int i = 0; i < textureData.Count; i++)
            {
                var cell = Instantiate(prefab.gameObject, parent).GetComponent<RacketLayoutChoiceIcon>();
                cell.InitializeChoiceElement(this);
                cell.SetData(_PartIndex, _PartToModify, textureData[i], "0");
                cell.SetComponentsSize(_GridLayoutGroup.cellSize.y);

                _Cells.Add(cell);
            }
        }
    }

    public void AddFinishOverlayToIcons(PremadeFinish finish)
    {
        foreach (var item in _Cells)
        {
            item.AddFinishOverlay(finish);
        }
    }

    public void ForceAnswer(int index)
    {
        _Cells[index].OnClick();
    }
    public void HideCell(int index)
    {
        _Cells[index].gameObject.SetActive(false);
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
            case DataTypeToLoad.Woods:
                return DataLoader.Instance.GetWoodData();
            case DataTypeToLoad.Rocks:
                return DataLoader.Instance.GetRocksData();
            case DataTypeToLoad.Scifi:
                return DataLoader.Instance.GetScifiData();
            case DataTypeToLoad.Fabrics:
                return DataLoader.Instance.GetFabricsData();
            case DataTypeToLoad.Organic:
                return DataLoader.Instance.GetOrganicData();
            case DataTypeToLoad.Military:
                return DataLoader.Instance.GetMilitaryData();
            case DataTypeToLoad.Animals:
                return DataLoader.Instance.GetAnimalsData();
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
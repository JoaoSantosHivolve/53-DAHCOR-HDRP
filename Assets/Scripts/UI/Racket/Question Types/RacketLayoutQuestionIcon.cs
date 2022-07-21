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
    [HideInInspector] public int selectedIndex;

    private GridLayoutGroup _GridLayoutGroup;
    private List<RacketLayoutChoiceIcon> _Cells;
    private int SPACING = 50;

    private void OnEnable()
    {
        //RefreshUi(); // Fixes visual bugs
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
                cell.index = i;
                cell.SetComponentsSize(_GridLayoutGroup.cellSize.y);

                if (GetComponent<RacketLayoutExtraEffect_AddIconPrices>() != null)
                {
                    var section = GetComponent<RacketLayoutExtraEffect_AddIconPrices>().section;
                    var price = PriceManager.Instance.GetPrice(section, RacketPriceCategory.Colormap);
                    cell.SetData(_PartIndex, _PartToModify, currentColorData.color, price);
                }
                else
                {
                    cell.SetData(_PartIndex, _PartToModify, currentColorData.color, 0);
                }

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
                cell.index = i;
                cell.SetComponentsSize(_GridLayoutGroup.cellSize.y);

                if (GetComponent<RacketLayoutExtraEffect_AddIconPrices>() != null)
                {
                    var section = GetComponent<RacketLayoutExtraEffect_AddIconPrices>().section;

                    if(dataToLoad == DataTypeToLoad.Precious)
                    {
                        var price = PriceManager.Instance.GetPrice(section, RacketPriceCategory.Precious, i);
                        cell.SetData(_PartIndex, _PartToModify, textureData[i], price);
                    }
                    else
                    {
                        var price = PriceManager.Instance.GetPrice(_DataTypeToLoad, section);
                        cell.SetData(_PartIndex, _PartToModify, textureData[i], price);
                    }
                }
                else
                {
                    cell.SetData(_PartIndex, _PartToModify, textureData[i], 0);
                }

                _Cells.Add(cell);
            }
        }

        (transform as RectTransform).sizeDelta = new Vector2(0,(_GridLayoutGroup.cellSize.y * ((_Cells.Count + 1) / 4)) + SPACING);
        (transform.Find("Grid") as RectTransform).sizeDelta = new Vector2(0, (_GridLayoutGroup.cellSize.y * ((_Cells.Count + 1) / 4)) + SPACING);
    }

    public void SetPrices(RacketPriceSection section)
    {
        foreach (var item in _Cells)
        {
            item.Price = PriceManager.Instance.GetPrice(_DataTypeToLoad, section);
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
    public Color GetSelectedColor()
    {
        return _Cells[selectedIndex]._DataColor;
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
    int GetRowCount(GridLayoutGroup glg)
    {
        var column = 0;
        var row = 0;

        if (glg.transform.childCount == 0)
            return 0;

        //Column and row are now 1
        column = 1;
        row = 1;

        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = glg.transform.
            GetChild(0).GetComponent<RectTransform>();

        Vector2 firstChildPos = firstChildObj.anchoredPosition;
        bool stopCountingRow = false;

        //Loop through the rest of the child object
        for (int i = 1; i < glg.transform.childCount; i++)
        {
            //Get the next child
            RectTransform currentChildObj = glg.transform.
           GetChild(i).GetComponent<RectTransform>();

            Vector2 currentChildPos = currentChildObj.anchoredPosition;

            //if first child.x == otherchild.x, it is a column, ele it's a row
            if (firstChildPos.x == currentChildPos.x)
            {
                column++;
                //Stop couting row once we find column
                stopCountingRow = true;
            }
            else
            {
                if (!stopCountingRow)
                    row++;
            }
        }

        return row;
    }
}
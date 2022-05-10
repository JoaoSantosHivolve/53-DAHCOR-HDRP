using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DataTypeToLoad
{
    NotSetYet,
    Colors
}

public class RacketLayoutQuestionIcon : RacketLayoutQuestion
{
    [SerializeField] private DataTypeToLoad _DataTypeToLoad = DataTypeToLoad.NotSetYet;
    private GridLayoutGroup _GridLayoutGroup;
    [SerializeField] private List<RacketLayoutChoiceIcon> _Cells;

    public override void Start()
    {
        base.Start();

        StartCoroutine(SetupGridLayoutGroup());
    }

    public override void Initialize()
    {
        if (transform.GetComponent<GridLayoutGroup>())
        {
            _GridLayoutGroup = transform.GetComponent<GridLayoutGroup>();
        }
        else if (transform.GetChild(1).GetComponent<GridLayoutGroup>())
        {
            _GridLayoutGroup = transform.GetChild(1).GetComponent<GridLayoutGroup>();
        }

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
        // Clear grid before populating with new data
        ClearGrid();

        // Populate grid
        var prefab = Resources.Load<RacketLayoutChoiceIcon>("Prefabs/Ui/IconDisplay");

        switch (_DataTypeToLoad)
        {
            case DataTypeToLoad.NotSetYet:
                break;
            case DataTypeToLoad.Colors:
                var data = DataLoader.Instance.GetColorData();
                var circleSprite = Resources.Load<Sprite>("Images/Circle");

                for (int i = 0; i < data.Count; i++)
                {
                    var currentData = data[i];
                    var cell = Instantiate(prefab.gameObject, _GridLayoutGroup.transform).GetComponent<RacketLayoutChoiceIcon>();
                    cell.ForceInitialize();
                    cell.SetData(circleSprite, currentData.name, currentData.color, currentData.price);

                    _Cells.Add(cell);
                }
                break;
            default:
                break;
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

    private IEnumerator SetupGridLayoutGroup()
    {
        yield return new WaitForEndOfFrame();

        var cellWidth = transform.GetComponent<RectTransform>().rect.width / 4f;
        _GridLayoutGroup.cellSize = new Vector2(cellWidth, cellWidth);
    }
    private void ClearGrid()
    {
        for (int i = 0; i < _GridLayoutGroup.transform.childCount; i++)
        {
            Destroy(_GridLayoutGroup.transform.GetChild(i).gameObject);
        }
    }
}
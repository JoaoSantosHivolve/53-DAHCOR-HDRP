using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropdownDataTypeToLoad
{
    None,
    CountryFlags
}

public class RacketLayoutQuestionDropdown : RacketLayoutQuestion
{
    [SerializeField] DropdownDataTypeToLoad _DataToLoad = DropdownDataTypeToLoad.None;
    private RacketLayoutChoiceDropdown _Choice;


    private void Awake()
    {
        _Choice  = transform.Find("Dropdown").GetComponentInChildren<RacketLayoutChoiceDropdown>();

        if (_Choice == null)
            Debug.LogError("Dropdown choice not found at: " + transform.name);
    }

    public override void Initialize()
    {
        _Choice.InitializeChoiceElement(this);
    }

    public override void OnReset()
    {
        _Answered = false;
    }

    public override void UpdateData()
    {
        if(_DataToLoad != DropdownDataTypeToLoad.None)
        {

        }
    }
}
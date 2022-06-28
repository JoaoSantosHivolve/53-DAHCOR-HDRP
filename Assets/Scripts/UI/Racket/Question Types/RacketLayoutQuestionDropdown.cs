using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if (_DataToLoad == DropdownDataTypeToLoad.CountryFlags)
        {
            var data = DataLoader.Instance.GetCountryFlagData();

            var dropdown = _Choice.GetDropdown();
            dropdown.ClearOptions();

            var list = new List<TMP_Dropdown.OptionData>();

            foreach (var item in data)
            {
                var flagString = item.image;
                var flagData = System.Convert.FromBase64String(flagString);
                var texture = new Texture2D(128, 128);
                texture.LoadImage(flagData);
                var flagSprite = Sprite.Create(texture,
                        new Rect(0.0f, 0.0f, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f), 100.0f);

                var itemData = new TMP_Dropdown.OptionData(item.name, flagSprite);
                list.Add(itemData);
            }

            dropdown.AddOptions(list);
        }
    }
}
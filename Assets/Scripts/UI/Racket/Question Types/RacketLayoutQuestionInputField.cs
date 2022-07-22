using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacketLayoutQuestionInputField : RacketLayoutQuestion
{
    private RacketLayoutChoiceInputField _InputField;

    public override void Initialize()
    {
        _InputField = transform.Find("InputField").GetComponent<RacketLayoutChoiceInputField>();
        _InputField.InitializeChoiceElement(this);
    }

    public override void OnReset()
    {
    }

    public override void UpdateData()
    {
    }

    public void ChangeText(string value)
    {
        _InputField.SetInputFieldText(value);
    }

    public TMP_InputField GetInputField()
    {
        return _InputField.GetInputField();
    }
}
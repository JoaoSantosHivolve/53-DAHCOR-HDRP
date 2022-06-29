using System.Collections;
using System.Collections.Generic;
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
}
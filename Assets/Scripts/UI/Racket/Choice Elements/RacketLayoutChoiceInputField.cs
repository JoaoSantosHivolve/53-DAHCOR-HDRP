using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacketLayoutChoiceInputField : RacketLayoutChoiceElement
{
    private TMP_InputField _InputField;

    public override void UpdateData()
    {
    }

    protected override void Initialize()
    {
        _InputField = GetComponent<TMP_InputField>();
        _InputField.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(string arg0)
    {
        if(arg0 != "")
        {
            if (_SetAnswered)
                _Question.SetAnswered();
        }

        // Send condition if any
        _Question.OnAnsweringQuestion();
    }
}
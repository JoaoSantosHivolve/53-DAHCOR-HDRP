using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacketLayoutChoiceInputField : RacketLayoutChoiceElement
{
    private TMP_InputField _InputField;

    protected override void Initialize()
    {
        _InputField = GetComponent<TMP_InputField>();
        _InputField.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(string arg0)
    {
        if(arg0 != "")
        {
            if (setAnswered)
                _Question.answered = true;
        }

        // Send condition if any
        _Question.OnChoiceClick(condition);
    }
}
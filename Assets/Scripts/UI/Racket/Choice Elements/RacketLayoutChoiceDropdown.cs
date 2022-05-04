using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacketLayoutChoiceDropdown : RacketLayoutChoiceElement
{
    private TMP_Dropdown _Dropdown;


    protected override void Initialize()
    {
        _Dropdown = GetComponent<TMP_Dropdown>();
        _Dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(int arg0)
    {
        // Set question answered
        if (setAnswered)
            _Question.answered = true;
    }
}
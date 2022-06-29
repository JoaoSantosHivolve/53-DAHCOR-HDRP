using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutChoiceCheckmark : RacketLayoutChoiceElement
{
    private Toggle _Toggle;

    protected override void Initialize()
    {
        _Toggle = GetComponent<Toggle>();
        _Toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool arg0)
    {
        (_Question as RacketLayoutQuestionCheckmark).SetEffect(GetCurrentState());
    }

    public bool GetCurrentState() => _Toggle.isOn;
}

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

        SetCheckmarkAnswerData();

        _Question.SetAnswered();
    }

    private void OnValueChanged(bool arg0)
    {
        SetCheckmarkAnswerData();

        _Question.SetAnswered();

        (_Question as RacketLayoutQuestionCheckmark).SetEffect(GetCurrentState());
    }

    public bool GetCurrentState() => _Toggle.isOn;

    private void SetCheckmarkAnswerData()
    {
        _Question.SetAnswerData(GetCurrentState() ? "Yes" : "No", GetCurrentState() ? PriceManager.Instance.GetPrice(RacketSpecificPriceSection.AutographYes) : 0);
    }
}

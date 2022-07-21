using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutQuestionCheckmark : RacketLayoutQuestion
{
    private RacketLayoutChoiceCheckmark _Toggle;

    [SerializeField] private AnswerEventGroup _OnEffect;
    [SerializeField] private AnswerEventGroup _OffEffect;

    private void Awake()
    {
        _Toggle = transform.Find("Toggle").GetComponentInChildren<RacketLayoutChoiceCheckmark>();
    }

    public override void Initialize()
    {
        _Toggle.InitializeChoiceElement(this);

        SetEffect(_Toggle.GetCurrentState());

        _Answered = true;
    }

    public override void OnReset()
    {
    }

    public override void UpdateData()
    {
    }


    public void SetEffect(bool value)
    {
        foreach (var item in value ? _OnEffect.events : _OffEffect.events) 
        {
            if(item.target != null)
                item.target.SetActive(item.active);
        }
    }
}
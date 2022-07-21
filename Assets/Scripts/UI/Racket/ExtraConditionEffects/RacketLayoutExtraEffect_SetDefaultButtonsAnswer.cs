using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_SetDefaultButtonsAnswer : RacketLayoutExtraEffect
{
    [SerializeField] private int _AnswerIndex;
    private RacketLayoutQuestionButtons _Question;

    public override void Initialize()
    {
        _Question = GetComponent<RacketLayoutQuestionButtons>();
    }

    public override void LateInitialize()
    {
        _Question.ForceAnswer(_AnswerIndex);
    }

    public override void OnClickEffect()
    {
    }
}
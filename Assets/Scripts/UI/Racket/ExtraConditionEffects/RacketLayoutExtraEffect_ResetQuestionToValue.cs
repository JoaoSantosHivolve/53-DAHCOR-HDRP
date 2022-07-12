using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_ResetQuestionToValue : RacketLayoutExtraEffect
{
    [SerializeField] private int _AnswerIndex;
    [SerializeField] private RacketLayoutQuestionButtons _TargetQuestion;

    public override void Initialize()
    {
    }
    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        _TargetQuestion.ForceAnswer(_AnswerIndex);
    }
}
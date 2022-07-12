using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_AnswerAgain : RacketLayoutExtraEffect
{
    [SerializeField] private RacketLayoutQuestionButtons _Question;

    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        _Question.ForceAnswer(_Question.GetSelectedAnswer());
    }
}
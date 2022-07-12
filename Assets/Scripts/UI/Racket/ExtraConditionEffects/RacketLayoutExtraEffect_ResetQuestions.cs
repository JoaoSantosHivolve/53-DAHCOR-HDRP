using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_ResetQuestions : RacketLayoutExtraEffect
{
    public List<RacketLayoutQuestion> targetQuestions;

    public override void Initialize()
    {
    }
    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        foreach (var item in targetQuestions)
        {
            item.ResetQuestion();
        }
    }
}
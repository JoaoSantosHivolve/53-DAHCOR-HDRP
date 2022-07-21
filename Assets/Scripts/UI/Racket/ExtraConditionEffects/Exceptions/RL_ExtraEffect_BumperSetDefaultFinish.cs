using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExtraEffect_BumperSetDefaultFinish : RacketLayoutExtraEffect
{
    private RacketLayoutQuestionButtons _Question;

    public override void Initialize()
    {
        _Question = transform.GetComponent<RacketLayoutQuestionButtons>();
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        if(_Question.GetSelectedAnswer() == 0)
        {
            RacketCostumizerController.Instance.SetFinish(PartToModify.Bumper, PremadeFinish.Matte);
        }
    }
}
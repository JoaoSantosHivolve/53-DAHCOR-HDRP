using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_ChangeButtcap : RacketLayoutExtraEffect
{
    private RacketLayoutQuestionButtons _Question;
    private int AnswerIndex
    {
        get { return _Question.GetSelectedAnswer(); }
    }

    private void Awake()
    {
        _Question = GetComponent<RacketLayoutQuestionButtons>();
    }

    public override void OnClickEffect()
    {
        if (AnswerIndex >= 0 && AnswerIndex < 2)
        {
            RacketCostumizerController.Instance.ChangeObject(PartToModify.Buttcap, DataLoader.Instance.GetButtcapData()[AnswerIndex]);
        }
    }
}
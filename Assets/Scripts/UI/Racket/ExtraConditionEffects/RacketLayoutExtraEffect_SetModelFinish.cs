using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_SetModelFinish : RacketLayoutExtraEffect
{
    private RacketLayoutQuestionButtons _Question;
    [SerializeField] private RacketLayoutQuestionIcon _QuestionIcon;
    [SerializeField] private PartToModify _PartToModify = PartToModify.None;

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
        if(AnswerIndex >= 0 && AnswerIndex < 4)
        {
            RacketCostumizerController.Instance.SetFinish(_PartToModify, (PremadeFinish)_Question.GetSelectedAnswer());
            _QuestionIcon.AddFinishOverlayToIcons((PremadeFinish)AnswerIndex);
        }
    }
}

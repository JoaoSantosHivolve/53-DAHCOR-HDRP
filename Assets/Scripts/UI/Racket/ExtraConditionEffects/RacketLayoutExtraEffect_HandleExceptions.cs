using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_HandleExceptions : RacketLayoutExtraEffect
{
    private RacketLayoutQuestionButtons _Question;

    [SerializeField] private RacketLayoutQuestionIcon _QuestionIcon;

    private const int BLACK_ID = 39;

    public override void Initialize()
    {
        _Question = transform.GetComponent<RacketLayoutQuestionButtons>();
    }

    public override void LateInitialize()
    {
        _QuestionIcon.HideCell(BLACK_ID);
    }

    public override void OnClickEffect()
    {
        if(_Question.GetSelectedAnswer() == 0)
        {
            _QuestionIcon.ForceAnswer(BLACK_ID);
        }
    }
}
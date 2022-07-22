using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExtraEffect_ForceAnswerOnClick : RacketLayoutExtraEffect
{
    [SerializeField] private int _OnAnsweringIndex = -1;
    [SerializeField] private int _AnswerIndex;
    [SerializeField] private RacketLayoutQuestion _TargetQuestion;
    private RacketLayoutQuestionButtons _Question;

    public override void Initialize()
    {
        _Question = GetComponent<RacketLayoutQuestionButtons>();
        if (_Question == null)
            Debug.Log("Invalid extra effect, force on click at: " + transform.name);
    }
    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        if(_OnAnsweringIndex == -1 || _OnAnsweringIndex == _Question.GetSelectedAnswer())
        {
            if (_TargetQuestion as RacketLayoutQuestionButtons != null)
            {
                (_TargetQuestion as RacketLayoutQuestionButtons).ForceAnswer(_AnswerIndex);
            }

            if (_TargetQuestion as RacketLayoutQuestionIcon != null)
            {
                (_TargetQuestion as RacketLayoutQuestionIcon).ForceAnswer(_AnswerIndex);
            }
        }
    }
}
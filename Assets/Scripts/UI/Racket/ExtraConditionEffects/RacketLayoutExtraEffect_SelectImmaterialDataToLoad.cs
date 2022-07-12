using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_SelectImmaterialDataToLoad : RacketLayoutExtraEffect
{
    private RacketLayoutQuestionButtons _Question;
    [SerializeField] private RacketLayoutQuestionIcon _ImmaterialQuestion;

    public override void Initialize()
    {
        _Question = transform.GetComponent<RacketLayoutQuestionButtons>();
    }
    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        _ImmaterialQuestion.InstantiateGrid(_Question.GetSelectedAnswer() + 2);
    }
}

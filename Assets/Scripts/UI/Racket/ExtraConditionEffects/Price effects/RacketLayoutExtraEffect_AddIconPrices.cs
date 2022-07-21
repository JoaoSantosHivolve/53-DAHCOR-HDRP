using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_AddIconPrices : RacketLayoutExtraEffect
{
    public RacketPriceSection section;
    private RacketLayoutQuestionIcon _IconQuestion;

    public override void Initialize()
    {
        _IconQuestion = transform.GetComponent<RacketLayoutQuestionIcon>();
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
    }

    public void UpdatePrices()
    {
        _IconQuestion.SetPrices(section);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExtraEffect_ChangeRacketText : RacketLayoutExtraEffect
{
    [SerializeField] private TextPlacement _Placement;

    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        RacketCostumizerController.Instance.ChangeText(_Placement, _BaseQuestion.GetAnswer());
    }
}

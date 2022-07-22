using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExtraEffect_ChangeRacketTextSizeBasedOnLength : RacketLayoutExtraEffect
{
    [SerializeField] private TextPlacement _Placement;
    private bool _AboveHalf;

    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        var text = (_BaseQuestion as RacketLayoutQuestionInputField).GetAnswer();

        var length = text.Length;
        var maxSize = (_BaseQuestion as RacketLayoutQuestionInputField).GetInputField().characterLimit;

        if(length > maxSize / 2f && !_AboveHalf)
        {
            _AboveHalf = true;

            //Set smaller
            RacketCostumizerController.Instance.AdjustTextSize(_Placement, false);
        }
        else if(length < maxSize / 2f && _AboveHalf)
        {
            _AboveHalf = false;

            //Set bigger
            RacketCostumizerController.Instance.AdjustTextSize(_Placement, true);
        }

    }
}

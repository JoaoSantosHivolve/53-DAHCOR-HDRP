using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExtraEffect_ChangeRacketFlag : RacketLayoutExtraEffect
{
    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        var answer = (_BaseQuestion as RacketLayoutQuestionDropdown).GetAnswer();

        var data = DataLoader.Instance.GetCountryFlagData();

        foreach (var item in data)
        {
            if(item.name == answer)
            {
                RacketCostumizerController.Instance.ChangeFlag(item);
            }
        }
    }
}

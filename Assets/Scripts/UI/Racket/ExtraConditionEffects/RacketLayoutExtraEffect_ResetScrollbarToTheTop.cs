using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// WAS USED ON OLD UI - NOT USED ANYMORE

// BUG FIX FOR THE CHANGE BETWEN "DAHCOR" "YOU" CHOICES
public class RacketLayoutExtraEffect_ResetScrollbarToTheTop : RacketLayoutExtraEffect
{
    public RacketLayoutQuestionButtons targetQuestion;
    public Scrollbar scrollbar;

    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        //if(targetQuestion.GetCurrentCondition() == Condition.ConditionTwo)
        //    scrollbar.value = 1;
    }
}

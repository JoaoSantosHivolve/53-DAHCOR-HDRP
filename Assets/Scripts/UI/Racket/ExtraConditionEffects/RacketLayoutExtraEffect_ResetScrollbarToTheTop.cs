using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BUG FIX FOR THE CHANGE BETWEN "DAHCOR" "YOU" CHOICES
public class RacketLayoutExtraEffect_ResetScrollbarToTheTop : RacketLayoutExtraEffect
{
    public RacketLayoutQuestion targetQuestion;
    public Scrollbar scrollbar;

    public override void OnClickEffect()
    {
        if(targetQuestion.GetCurrentCondition() == Condition.ConditionTwo)
            scrollbar.value = 1;
    }
}

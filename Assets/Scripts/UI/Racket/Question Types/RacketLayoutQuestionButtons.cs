using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutQuestionButtons : RacketLayoutQuestion
{
    public override void UpdateData()
    {

    }

    public override void OnReset()
    {
        foreach (RacketLayoutChoiceButton item in _Choices)
        {
            item.SetUnselected();
        }
    }
    public void ClearOtherSelectedButtons(RacketLayoutChoiceButton button)
    {
        foreach (RacketLayoutChoiceButton item in _Choices)
        {
            if (item != button)
            {
                item.SetUnselected();
            }
        }
    }
}
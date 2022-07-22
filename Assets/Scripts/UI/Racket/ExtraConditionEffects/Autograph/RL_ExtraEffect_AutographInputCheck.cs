using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ExtraEffect_AutographInputCheck : RacketLayoutExtraEffect
{

    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        var text = (_BaseQuestion as RacketLayoutQuestionInputField).GetAnswer();

        if (text == "")
            return;

        var textArray = text.ToCharArray();

        for (int i = 0; i < textArray.Length; i++)
        {
            if (textArray[i].ToString() == "")
                continue;

            // If first letter, or new word
            if(i == 0 || (i > 1 && textArray[i - 1].ToString() == " "))
                textArray[i] = char.ToUpper(textArray[i]);
            else
                textArray[i] = char.ToLower(textArray[i]);
        }

        var newText = new string(textArray);

        (_BaseQuestion as RacketLayoutQuestionInputField).ChangeText(newText);
    }
}

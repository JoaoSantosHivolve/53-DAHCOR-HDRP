using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_SetDefaultColormap : RacketLayoutExtraEffect
{
    private RacketLayoutQuestionButtons _Question;
    public RacketPriceSection section;
    public List<RacketLayoutQuestionIcon> iconSelectors;

    public override void Initialize()
    {
        _Question = transform.GetComponent<RacketLayoutQuestionButtons>();
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
        if(section == RacketPriceSection.Body_Minimal)
        {
            if (_Question.GetSelectedAnswer() == 0)
            {
                iconSelectors[0].ForceAnswer(0); // set white
            }
        }
        if (section == RacketPriceSection.Body_Outline_OffBeat)
        {
            if (_Question.GetSelectedAnswer() == 0)
            {
                iconSelectors[0].ForceAnswer(0); // set white
                iconSelectors[1].ForceAnswer(37); // set grey
            }
        }
        if (section == RacketPriceSection.Head_Mono_Break)
        {
            if (_Question.GetSelectedAnswer() == 0)
            {
                iconSelectors[0].ForceAnswer(0); // set white
            }
        }
        if (section == RacketPriceSection.Head_Deuce)
        {
            if (_Question.GetSelectedAnswer() == 0)
            {
                iconSelectors[0].ForceAnswer(0); // set white
                iconSelectors[1].ForceAnswer(37); // set grey
            }
        }
    }
}
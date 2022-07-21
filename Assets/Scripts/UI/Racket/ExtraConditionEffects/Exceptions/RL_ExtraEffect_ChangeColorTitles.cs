using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RL_ExtraEffect_ChangeColorTitles : RacketLayoutExtraEffect
{
    public RacketPriceSection section;

    public TextMeshProUGUI title1;
    public TextMeshProUGUI title2;
    public TextMeshProUGUI titleMat1;
    public TextMeshProUGUI titleMat2;

    public RacketLayoutQuestionButtons bodyLayoutQuestion;

    public override void Initialize()
    {
    }

    public override void LateInitialize()
    {
    }

    public override void OnClickEffect()
    {
    }

    public void UpdateTitles()
    {
        switch (section)
        {
            case RacketPriceSection.NotSetYet:
                break;
            case RacketPriceSection.Body_Minimal:
                title1.text = "Color:";
                title2.text = "Color:";
                titleMat1.text = "Material:";
                titleMat2.text = "Material:";
                break;
            case RacketPriceSection.Body_Outline_OffBeat:
                if(bodyLayoutQuestion.GetSelectedAnswer() == 2)
                {
                    title1.text = "Side 1:";
                    title2.text = "Side 2:";
                    titleMat1.text = "Side 1:";
                    titleMat2.text = "Side 2:";
                }
                else
                {
                    title1.text = "Interior:";
                    title2.text = "Profile:";
                    titleMat1.text = "Interior:";
                    titleMat2.text = "Profile:";
                }
                break;
            case RacketPriceSection.Head_Mono_Break:
                title1.text = "Color:";
                title2.text = "Color:";
                titleMat1.text = "Material:";
                titleMat2.text = "Material:";
                break;
            case RacketPriceSection.Head_Deuce:
                title1.text = "Side 1:";
                title2.text = "Side 2:";
                titleMat1.text = "Side 1:";
                titleMat2.text = "Side 2:";
                break;
            default:
                break;
        }
     
    }
}
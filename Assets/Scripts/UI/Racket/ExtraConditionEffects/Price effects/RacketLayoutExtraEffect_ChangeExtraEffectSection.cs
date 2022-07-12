using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutExtraEffect_ChangeExtraEffectSection : RacketLayoutExtraEffect
{
    public bool body;
    private RacketLayoutQuestionButtons _Question;
    private int AnswerIndex
    {
        get { return _Question.GetSelectedAnswer(); }
    }
    
    public RacketLayoutExtraEffectAddButtonPrices _ExtraEffect;

    public override void Initialize()
    {
        _Question = GetComponent<RacketLayoutQuestionButtons>();
    }

    public override void LateInitialize()
    {

    }

    public override void OnClickEffect()
    {
        if (body)
        {
            if (AnswerIndex == 0)
            {
                _ExtraEffect.section = RacketPriceSection.Body_Minimal;
            }
            else
            {
                _ExtraEffect.section = RacketPriceSection.Body_Outline_OffBeat;
            }
        }
        // head
        else
        {
            if(AnswerIndex == 0)
            {
                _ExtraEffect.section = RacketPriceSection.NotSetYet;
            }
            else if(AnswerIndex > 0 && AnswerIndex < 2)
            {
                _ExtraEffect.section = RacketPriceSection.Head_Mono_Break;
            }
            else
            {
                _ExtraEffect.section = RacketPriceSection.Head_Deuce;
            }
        }
        
       
        _ExtraEffect.UpdatePrices();
    }
}
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

    public List<RacketLayoutExtraEffect_AddIconPrices> _IconQuestions;
    public List<RacketLayoutExtraEffect_SetDefaultColormap> _ChangeColormaps;
    public RL_ExtraEffect_ChangeColorTitles _ColorTitles;

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
                if (_ExtraEffect != null) _ExtraEffect.section = RacketPriceSection.Body_Minimal;
                foreach (var item in _IconQuestions)
                {
                    item.section = RacketPriceSection.Body_Minimal;
                }
                foreach (var item in _ChangeColormaps)
                {
                    item.section = RacketPriceSection.Body_Minimal;
                }

                if (_ColorTitles != null) _ColorTitles.section = RacketPriceSection.Body_Minimal;
            }
            else
            {
                _ExtraEffect.section = RacketPriceSection.Body_Outline_OffBeat;
                foreach (var item in _IconQuestions)
                {
                    item.section = RacketPriceSection.Body_Outline_OffBeat;
                }
                foreach (var item in _ChangeColormaps)
                {
                    item.section = RacketPriceSection.Body_Outline_OffBeat;
                }

                if (_ColorTitles != null) _ColorTitles.section = RacketPriceSection.Body_Outline_OffBeat;
            }
        }
        // head
        else 
        {
            if(AnswerIndex == 0)
            {
                if (_ExtraEffect != null) _ExtraEffect.section = RacketPriceSection.NotSetYet;
                foreach (var item in _IconQuestions)
                {
                    item.section = RacketPriceSection.NotSetYet;
                }
                if (_ColorTitles != null) _ColorTitles.section = RacketPriceSection.NotSetYet;
            }
            else if(AnswerIndex == 1 || AnswerIndex == 3)
            {
                if (_ExtraEffect != null) _ExtraEffect.section = RacketPriceSection.Head_Mono_Break;
                foreach (var item in _IconQuestions)
                {
                    item.section = RacketPriceSection.Head_Mono_Break;
                }
                if (_ColorTitles != null) _ColorTitles.section = RacketPriceSection.Head_Mono_Break;
            }
            else
            {
                if (_ExtraEffect != null) _ExtraEffect.section = RacketPriceSection.Head_Deuce;
                foreach (var item in _IconQuestions)
                {
                    item.section = RacketPriceSection.Head_Deuce;
                }
                if (_ColorTitles != null) _ColorTitles.section = RacketPriceSection.Head_Deuce;
            }
        }


        if (_ExtraEffect != null) _ExtraEffect.UpdatePrices();
        foreach (var item in _IconQuestions)
        {
            item.UpdatePrices();
        }
        if (_ColorTitles != null) _ColorTitles.UpdateTitles();
    }
}
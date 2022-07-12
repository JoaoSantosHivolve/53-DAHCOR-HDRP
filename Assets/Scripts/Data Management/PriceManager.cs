using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RacketPriceSection
{
    NotSetYet,
    Body_Minimal,
    Body_Outline_OffBeat,
    Head_Mono_Break,
    Head_Deuce,
    Bumper,
    Handle
}

public enum RacketSpecificPriceSection
{
    BodyMinimal,
    BodyOutline,
    BodyOffbeat,
    HeadNoCostumization,
    HeadMono,
    HeadBreak,
    HeadDeuce,
    LetteringGloss,
    LetteringMatte,
    LetteringMetal,
    LetteringChrome,
    ButtcapStandart,
    ButtcapPro,
    StringsNoCostumization,
    StringsColormap,
    AutographYes
}

public enum RacketPriceCategory
{
    Colormap,
    Gloss,
    Matte,
    Metal,
    Chrome,
    Woods,
    Fabrics,
    Rocks,
    Military,
    Scifi,
    Animals,
    Organic,
    Precious
}

public class PriceManager : Singleton<PriceManager>
{
    private List<PriceData> _PriceData { get => DataLoader.Instance.GetPriceData(); }
    private List<SpecificPriceData> _SpecificPriceData { get => DataLoader.Instance.GetSpecificPriceData(); }

    public int GetPrice(RacketPriceSection section, RacketPriceCategory category)
    {
        var cell = _PriceData[(int)category];
        return GetPriceFromSection(cell, section);
    }
    public int GetPrice(RacketSpecificPriceSection section)
    {
        switch (section)
        {
            case RacketSpecificPriceSection.BodyMinimal:
                return _SpecificPriceData[0].body_minimal;
            case RacketSpecificPriceSection.BodyOutline:
                return _SpecificPriceData[0].body_outline;
            case RacketSpecificPriceSection.BodyOffbeat:
                return _SpecificPriceData[0].body_offbeat;

            case RacketSpecificPriceSection.HeadNoCostumization:
                return _SpecificPriceData[0].head_no_costumization;
            case RacketSpecificPriceSection.HeadMono:
                return _SpecificPriceData[0].head_mono;
            case RacketSpecificPriceSection.HeadBreak:
                return _SpecificPriceData[0].head_break;
            case RacketSpecificPriceSection.HeadDeuce:
                return _SpecificPriceData[0].head_deuce;

            case RacketSpecificPriceSection.LetteringGloss:
                return _SpecificPriceData[0].lettering_gloss;
            case RacketSpecificPriceSection.LetteringMatte:
                return _SpecificPriceData[0].lettering_matte;
            case RacketSpecificPriceSection.LetteringMetal:
                return _SpecificPriceData[0].lettering_metal;
            case RacketSpecificPriceSection.LetteringChrome:
                return _SpecificPriceData[0].lettering_chrome;

            case RacketSpecificPriceSection.ButtcapStandart:
                return _SpecificPriceData[0].buttcap_standart;
            case RacketSpecificPriceSection.ButtcapPro:
                return _SpecificPriceData[0].buttcap_pro;

            case RacketSpecificPriceSection.StringsNoCostumization:
                return _SpecificPriceData[0].strings_no_costumization;
            case RacketSpecificPriceSection.StringsColormap:
                return _SpecificPriceData[0].strings_colormap;

            case RacketSpecificPriceSection.AutographYes:
                return _SpecificPriceData[0].autograph_yes;

            default:
                return 0;
        }
    }
    public int GetPrice(RacketPriceSection section, RacketPriceCategory category, int index) // USED FOR PRECIOUS
    {
        var cell = _PriceData[(int)category + index];
        return GetPriceFromSection(cell, section);
    }
    private int GetPriceFromSection(PriceData cell, RacketPriceSection section)
    {
        switch (section)
        {
            case RacketPriceSection.NotSetYet:
                return 0;
            case RacketPriceSection.Body_Minimal:
                return cell.body_minimal;
            case RacketPriceSection.Body_Outline_OffBeat:
                return cell.body_outline_offbeat;
            case RacketPriceSection.Head_Mono_Break:
                return cell.head_mono_break;
            case RacketPriceSection.Head_Deuce:
                return cell.head_deuce;
            case RacketPriceSection.Bumper:
                return cell.bumper;
            case RacketPriceSection.Handle:
                return cell.handle;

                default: return 0;
        }
    }

    public RacketPriceCategory GetPriceCategory(int value)
    {
        var index = value - 1;

        if(index <= 12)
        {
            return (RacketPriceCategory)index;
        }
        else
        {
            return RacketPriceCategory.Precious;
        }
    }
}
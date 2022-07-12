using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ButtonPriceTag
{
    public RacketLayoutChoiceButton button;
    public RacketPriceCategory category;
}
[Serializable]
public struct SpecificButtonPriceTag
{
    public RacketLayoutChoiceButton button;
    public RacketSpecificPriceSection section;
}

public class RacketLayoutExtraEffectAddButtonPrices : RacketLayoutExtraEffect
{
    [Header("General")]
    public RacketPriceSection section;
    public List<ButtonPriceTag> buttonsToPrice;
    [Header("Specific")]
    public List<SpecificButtonPriceTag> buttonsToSpecificPrice;

    public override void Initialize()
    {
    }
    public override void LateInitialize()
    {
        UpdatePrices();
    }
    public override void OnClickEffect()
    {
    }

    public void UpdatePrices()
    {
        foreach (var item in buttonsToPrice)
        {
            item.button.Price = PriceManager.Instance.GetPrice(section, item.category);
        }

        foreach (var item in buttonsToSpecificPrice)
        {
            item.button.Price = PriceManager.Instance.GetPrice(item.section);
        }
    }
}
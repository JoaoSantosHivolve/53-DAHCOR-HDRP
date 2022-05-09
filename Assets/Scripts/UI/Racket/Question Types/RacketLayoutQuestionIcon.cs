using UnityEngine;

public enum IconData
{
    NotSetYet,
    Colors
}

public class RacketLayoutQuestionIcon : RacketLayoutQuestion
{
    [SerializeField] private IconData _IconData;

    public override void OnReset()
    {
        foreach (RacketLayoutChoiceIcon item in _Choices)
        {
            item.SetUnselected();
        }
    }
    public override void UpdateData()
    {
        // TODO::
    }

    public void ClearOtherSelectedIcons(RacketLayoutChoiceIcon icon)
    {
        foreach (RacketLayoutChoiceIcon item in _Choices)
        {
            if (item != icon)
                item.SetUnselected();
        }
    }
}
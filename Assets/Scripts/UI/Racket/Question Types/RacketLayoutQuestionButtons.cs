using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutQuestionButtons : RacketLayoutQuestion
{
    [SerializeField] private Condition _SelectedCondition = Condition.NoConditionSetYet;
    [SerializeField] private List<GameObject> _Choice1;
    [SerializeField] private List<GameObject> _Choice2;
    [SerializeField] private List<GameObject> _Choice3;
    [SerializeField] private List<GameObject> _Choice4;
    [SerializeField] private List<GameObject> _AlwaysClearOnClick;
    private List<RacketLayoutChoiceButton> _ChoiceButtons;

    public override void Start()
    {
        base.Start();

        ClearChoices();
    }
    private void OnEnable()
    {
        if (!_Answered)
            ClearChoices();
        if (_Answered)
            SetChoice();
    }

    public override void Initialize()
    {
        _ChoiceButtons = new List<RacketLayoutChoiceButton>();
    }
    public override void UpdateData()
    {

    }
    public override void OnReset()
    {
        foreach (RacketLayoutChoiceButton item in _ChoiceButtons)
        {
            item.SetUnselected();
        }
    }
    
    public void ClearOtherSelectedButtons(RacketLayoutChoiceButton button)
    {
        foreach (RacketLayoutChoiceButton item in _ChoiceButtons)
        {
            if (item != button)
            {
                item.SetUnselected();
            }
        }
    }
    public void OnSelectingChoice(Condition condition)
    {
        _SelectedCondition = condition;

        ClearChoices();

        if (_SelectedCondition != Condition.NoCondition)
            SetChoice();

        OnAnsweringQuestion();
    }
    public Condition GetCurrentCondition() => _SelectedCondition;
    public void AddChoiceElement(RacketLayoutChoiceButton choiceButton) => _ChoiceButtons.Add(choiceButton);

    private void ClearChoices()
    {
        foreach (var item in _Choice1)
            item.SetActive(false);
        foreach (var item in _Choice2)
            item.SetActive(false);
        foreach (var item in _Choice3)
            item.SetActive(false);
        foreach (var item in _Choice4)
            item.SetActive(false);
        foreach (var item in _AlwaysClearOnClick)
            item.SetActive(false);
    }
    private void SetChoice()
    {
        switch (_SelectedCondition)
        {
            case Condition.ConditionOne:
                foreach (var item in _Choice1)
                    item.SetActive(true);
                break;
            case Condition.ConditionTwo:
                foreach (var item in _Choice2)
                    item.SetActive(true);
                break;
            case Condition.ConditionThree:
                foreach (var item in _Choice3)
                    item.SetActive(true);
                break;
            case Condition.ConditionFour:
                foreach (var item in _Choice4)
                    item.SetActive(true);
                break;
            default:
                break;
        }
    }
}
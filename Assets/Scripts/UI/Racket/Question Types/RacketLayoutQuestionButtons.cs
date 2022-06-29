using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AnswerEventGroup
{
    public string name; // used only to make inspector easier to read
    public List<AnswerEvent> events;
}
[Serializable]
public struct AnswerEvent
{
    public GameObject target;
    public bool active;
}

public class RacketLayoutQuestionButtons : RacketLayoutQuestion
{
    //[SerializeField] private Condition _SelectedCondition = Condition.NoConditionSetYet;
    //[SerializeField] private List<GameObject> _Choice1;
    //[SerializeField] private List<GameObject> _Choice2;
    //[SerializeField] private List<GameObject> _Choice3;
    //[SerializeField] private List<GameObject> _Choice4;
    //[SerializeField] private List<GameObject> _AlwaysClearOnClick;
    private RacketLayoutChoiceButton[] _ChoiceButtons;

    private int _SelectedAnswer = 99;
    [SerializeField] private AnswerEventGroup[] _AnswerEvents;
    [SerializeField] private AnswerEventGroup _OnInitializeEvent;

    private void Awake()
    {
        _ChoiceButtons = transform.Find("Options").GetComponentsInChildren<RacketLayoutChoiceButton>();
    }

    private void OnEnable()
    {
        if (!_Initialized)
            return;

        if (_Answered)
            SetChoice(_SelectedAnswer);
    }

    public override void Initialize()
    {
        for (int i = 0; i < _ChoiceButtons.Length; i++)
        {
            _ChoiceButtons[i].InitializeChoiceElement(this);
            _ChoiceButtons[i].SetIndex(i);
        }

        for (int i = 0; i < _OnInitializeEvent.events.Count; i++)
        {
            var aEvent = _OnInitializeEvent.events[i];
            aEvent.target.SetActive(aEvent.active);
        }

        OnEnable();
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
    public void OnSelectingChoice(int answer)
    {
        if (_SelectedAnswer != answer)
            SetChoice(answer);

        _SelectedAnswer = answer;

        OnAnsweringQuestion();
    }
    
    private void SetChoice(int answer)
    {
        if (_AnswerEvents.Length <= answer)
            return;

        for (int i = 0; i < _AnswerEvents[answer].events.Count; i++)
        {
            var aEvent = _AnswerEvents[answer].events[i];
            
            if(aEvent.target != null)
                aEvent.target.SetActive(aEvent.active);
        }
    }
}
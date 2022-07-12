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
    [SerializeField] private string _Answer;
    [SerializeField] private int _Cost;

    private int _SelectedAnswer = 99;
    private RacketLayoutChoiceButton[] _ChoiceButtons;
    [SerializeField] private AnswerEventGroup[] _AnswerEvents;
    [SerializeField] private AnswerEventGroup _OnInitializeEvent;

    private void Awake()
    {
        _ChoiceButtons = transform.Find("Options").GetComponentsInChildren<RacketLayoutChoiceButton>();
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
    }
    public void ForceAnswer(int index)
    {
        if(_ChoiceButtons[index] == null)
        {
            Debug.Log("Index out of bounds");
            return;
        }

        _ChoiceButtons[index].OnClick();
    }
    public void SetAnswerData(string answer, int cost)
    {
        _Answer = answer;
        _Cost = cost;
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

    public int GetSelectedAnswer() => _SelectedAnswer;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacketLayoutQuestion : MonoBehaviour
{
    public bool answered;

    private List<RacketLayoutChoiceButton> _ChoiceButtons = new List<RacketLayoutChoiceButton>();
    private List<RacketLayoutChoiceIcon> _ChoiceIcons = new List<RacketLayoutChoiceIcon>();
    public List<GameObject> choice1;
    public List<GameObject> choice2;
    public List<GameObject> choice3;
    public List<GameObject> choice4;
    public List<GameObject> alwaysClearOnClick;


    private Condition _QuestionCondition = Condition.NoConditionSetYet;
    private RacketLayoutQuestionController _Controller;
    private RacketLayoutExtraEffect[] _ExtraEffects;

    private void Awake()
    {
        _Controller = transform.parent.GetComponent<RacketLayoutQuestionController>();
        _ExtraEffects = transform.GetComponents<RacketLayoutExtraEffect>();
    }
    private void Start()
    {
        ClearChoices();
    }

    public void AddButton(RacketLayoutChoiceButton button)
    {
        _ChoiceButtons.Add(button);
    }
    public void ClearOtherSelectedButtons(RacketLayoutChoiceButton button)
    {
        foreach (var item in _ChoiceButtons)
        {
            if (item != button)
                item.SetUnselected();
        }
    }
    public void OnChoiceClick(Condition condition)
    {
        _QuestionCondition = condition;

        // Reset choices state
        ClearChoices();

        SetChoices();

        // If extra effect scripts are added to gameobject, they activate now
        if(_ExtraEffects.Length > 0)
        {
            foreach (var item in _ExtraEffects)
            {
                item.OnClickEffect();
            }
        }

        _Controller.RefreshLayoutGroup();

        // Check all answeres
        _Controller.CheckIfAllQuestionsAreAnswered();
    }

    public void AddIcon(RacketLayoutChoiceIcon icon) => _ChoiceIcons.Add(icon);
    public void ClearOtherSelectedIcons(RacketLayoutChoiceIcon icon)
    {
        foreach (var item in _ChoiceIcons)
        {
            if (item != icon)
                item.SetUnselected();
        }
    }

    private void ClearChoices()
    {
        foreach (var item in choice1)
        {
            item.SetActive(false);
        }
        foreach (var item in choice2)
        {
            item.SetActive(false);
        }
        foreach (var item in choice3)
        {
            item.SetActive(false);
        }
        foreach (var item in choice4)
        {
            item.SetActive(false);
        }
        foreach (var item in alwaysClearOnClick)
        {
            item.SetActive(false);
        }
    }
    private void SetChoices()
    {
        switch (_QuestionCondition)
        {
            case Condition.ConditionOne:
                foreach (var item in choice1)
                {
                    item.SetActive(true);
                }
                break;
            case Condition.ConditionTwo:
                foreach (var item in choice2)
                {
                    item.SetActive(true);
                }
                break;
            case Condition.ConditionThree:
                foreach (var item in choice3)
                {
                    item.SetActive(true);
                }
                break;
            case Condition.ConditionFour:
                foreach (var item in choice4)
                {
                    item.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {
        if (!answered)
            ClearChoices();
        if (answered)
            SetChoices();
    }

    public void ResetQuestion()
    {
        answered = false;

        foreach (var item in _ChoiceButtons)
        {
            item.SetUnselected();
        }
        foreach (var item in _ChoiceIcons)
        {
            item.SetUnselected();
        }
    }
    public Condition GetCurrentCondition() => _QuestionCondition;
}
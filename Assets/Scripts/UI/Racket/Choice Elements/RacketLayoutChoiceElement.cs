using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Condition
{
    NoCondition,
    ConditionOne,
    ConditionTwo,
    ConditionThree,
    ConditionFour,
    NoConditionSetYet
}

public abstract class RacketLayoutChoiceElement : MonoBehaviour
{
    [SerializeField] protected Condition _Condition = Condition.NoCondition;
    [SerializeField] protected bool _SetAnswered = true;
    protected RacketLayoutQuestion _Question;

    private void Awake()
    {
        if (FindQuestion())
        {
            Initialize();
        }
        else Debug.Log("Question not found at " + this.name);
    }

    protected abstract void Initialize();
    public abstract void UpdateData();

    protected bool FindQuestion()
    {
        if (transform.parent.GetComponent<RacketLayoutQuestion>() != null)
        {
            _Question = transform.parent.GetComponent<RacketLayoutQuestion>();
            return true;
        }
        else if (transform.parent.parent.GetComponent<RacketLayoutQuestion>() != null)
        {
            _Question = transform.parent.parent.GetComponent<RacketLayoutQuestion>();
            return true;
        }
        return false;
    }
}
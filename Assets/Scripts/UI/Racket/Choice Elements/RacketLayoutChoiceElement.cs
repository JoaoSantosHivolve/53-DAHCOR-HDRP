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
    protected RacketLayoutQuestion _Question;
    public Condition condition = Condition.NoCondition;
    public bool setAnswered = true;

    private void Awake()
    {
        if (FindQuestion())
        {
            Initialize();
        }
        else Debug.Log("Question not found at " + this.name);
    }

    protected abstract void Initialize();

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

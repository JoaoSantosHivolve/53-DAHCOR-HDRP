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
    [SerializeField] protected bool _SetAnswered = true;
    protected bool _Initialized = false;
    protected RacketLayoutQuestion _Question;

    private void Awake()
    {
        if (FindQuestion() && !_Initialized)
        {
            Initialize();

            _Initialized = true;
        }
        else if (!FindQuestion())
            Debug.Log("Question not found at " + this.name);
    }

    protected abstract void Initialize();

    protected bool FindQuestion()
    {
        if (transform.GetComponent<RacketLayoutQuestion>() != null)
        {
            _Question = transform.GetComponent<RacketLayoutQuestion>();
            return true;
        }
        else if (transform.parent.GetComponent<RacketLayoutQuestion>() != null)
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

    public void ForceInitialize()
    {
        Initialize();
        
        _Initialized = true;
    }
}
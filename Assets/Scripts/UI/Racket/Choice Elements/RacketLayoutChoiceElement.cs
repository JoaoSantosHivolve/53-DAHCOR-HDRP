using UnityEngine;

public abstract class RacketLayoutChoiceElement : MonoBehaviour
{
    //[SerializeField] protected bool _SetAnswered = true;
    protected bool _Initialized = false;
    protected RacketLayoutQuestion _Question;

    public void InitializeChoiceElement(RacketLayoutQuestion question)
    {
        _Question = question;

        Initialize();
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
}
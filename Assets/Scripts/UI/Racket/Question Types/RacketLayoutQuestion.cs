using System.Collections.Generic;
using UnityEngine;

public abstract class RacketLayoutQuestion : MonoBehaviour
{
    [SerializeField] private bool _Answered;
    [SerializeField] private Condition _SelectedCondition = Condition.NoConditionSetYet;
    [SerializeField] private List<GameObject> _Choice1;
    [SerializeField] private List<GameObject> _Choice2;
    [SerializeField] private List<GameObject> _Choice3;
    [SerializeField] private List<GameObject> _Choice4;
    [SerializeField] private List<GameObject> _AlwaysClearOnClick;
    protected List<RacketLayoutChoiceElement> _Choices;

    private RacketLayoutQuestionController _Controller;
    protected RacketLayoutExtraEffect[] _ExtraEffects;

    private void Awake()
    {
        _Choices = new List<RacketLayoutChoiceElement>();
        _Controller = transform.parent.GetComponent<RacketLayoutQuestionController>();
        _ExtraEffects = transform.GetComponents<RacketLayoutExtraEffect>();
    }
    private void Start()
    {
        ClearChoices();
    }
    private void OnEnable()
    {
        if (!_Answered)
            ClearChoices();
        if (_Answered)
            SetChoice();
    }

    public abstract void UpdateData();
    public abstract void OnReset();

    public void OnChoiceClick(Condition condition)
    {
        _SelectedCondition = condition;

        ClearChoices();

        if(_SelectedCondition != Condition.NoCondition)
            SetChoice();

        // If extra effect scripts are added to gameobject, they activate now
        if(_ExtraEffects.Length > 0)
        {
            foreach (var item in _ExtraEffects)
            {
                item.OnClickEffect();
            }
        }

        _Controller.RefreshLayoutGroup();

        _Controller.CheckIfAllQuestionsAreAnswered();
    }

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

    public void ResetQuestion()
    {
        _Answered = false;

        OnReset();
    }
    public void SetAnswered() => _Answered = true;
    public void AddChoiceElement(RacketLayoutChoiceElement choiceElement) => _Choices.Add(choiceElement);
    public Condition GetCurrentCondition() => _SelectedCondition;

    //[ContextMenu("Change Font Size")]
    //void ChangeFontSize()
    //{
    //    if(transform.GetChild(0).GetComponent<TextMeshProUGUI>() != null)
    //    {
    //        transform.GetChild(0).GetComponent<TextMeshProUGUI>().enableAutoSizing = false;
    //        transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 23;
    //        transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
    //
    //        transform.GetChild(0).GetComponent<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Bottom;
    //    }
    //}
}
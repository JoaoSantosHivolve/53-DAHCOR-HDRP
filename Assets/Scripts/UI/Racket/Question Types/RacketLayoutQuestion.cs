using System.Collections.Generic;
using UnityEngine;

public abstract class RacketLayoutQuestion : MonoBehaviour
{
    [SerializeField] protected bool _Answered;
    protected List<RacketLayoutChoiceElement> _ChoiceElements;

    private RacketLayoutQuestionController _Controller;
    protected RacketLayoutExtraEffect[] _ExtraEffects;

    private void Awake()
    {
        _ChoiceElements = new List<RacketLayoutChoiceElement>();
        _Controller = transform.parent.GetComponent<RacketLayoutQuestionController>();
        _ExtraEffects = transform.GetComponents<RacketLayoutExtraEffect>();
    }

    public abstract void UpdateData();
    public abstract void OnReset();

    public void OnAnsweringQuestion()
    {
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

    public void ResetQuestion()
    {
        _Answered = false;

        OnReset();
    }
    public void SetAnswered() => _Answered = true;
    public void AddChoiceElement(RacketLayoutChoiceElement choiceElement) => _ChoiceElements.Add(choiceElement);
    public bool IsAnswered => _Answered;

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
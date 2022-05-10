using System.Collections.Generic;
using UnityEngine;

public abstract class RacketLayoutQuestion : MonoBehaviour
{
    [SerializeField] protected bool _Answered;
    private RacketLayoutQuestionController _Controller;
    private RacketLayoutExtraEffect[] _ExtraEffects;

    public void BaseInitialize()
    {
        _Controller = transform.parent.GetComponent<RacketLayoutQuestionController>();
        _ExtraEffects = transform.GetComponents<RacketLayoutExtraEffect>();

        Initialize();
    }
    public virtual void Start()
    {
    }

    public abstract void Initialize();
    public abstract void UpdateData();
    public abstract void OnReset();

    public void OnAnsweringQuestion()
    {
        // If extra effect scripts are added to gameobject, they activate now
        if (_ExtraEffects != null)
        {
            if (_ExtraEffects.Length > 0)
            {
                foreach (var item in _ExtraEffects)
                {
                    item.OnClickEffect();
                }
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
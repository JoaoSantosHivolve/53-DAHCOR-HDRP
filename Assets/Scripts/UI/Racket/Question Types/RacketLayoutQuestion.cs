using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RacketLayoutQuestion : MonoBehaviour
{
    [SerializeField] protected bool _Answered;
    private RacketLayoutQuestionController _Controller;
    private RacketLayoutExtraEffect[] _ExtraEffects;
    protected bool _Initialized = false;

    public void BaseInitialize()
    {
        _Initialized = true;
        _Controller = transform.parent.GetComponent<RacketLayoutQuestionController>();
        _ExtraEffects = transform.GetComponents<RacketLayoutExtraEffect>();

        Initialize();
    }

    public abstract void Initialize();
    public abstract void UpdateData();
    public abstract void OnReset();

    public void ResetQuestion()
    {
        _Answered = false;

        OnReset();
    }
    public void SetAnswered()
    {
        _Answered = true;

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

        _Controller.CheckIfAllQuestionsAreAnswered();

        RefreshUi(); // Fixes some visual bugs
    }

    public bool IsAnswered => _Answered;

    protected void RefreshUi()
    {
        // Actualy works pretty well 
        Canvas.ForceUpdateCanvases();
        GetComponent<VerticalLayoutGroup>().spacing += 0.001f;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        Canvas.ForceUpdateCanvases();

        Canvas.ForceUpdateCanvases();
        GetComponent<VerticalLayoutGroup>().spacing += 0.001f;
        Canvas.ForceUpdateCanvases();
    }
}
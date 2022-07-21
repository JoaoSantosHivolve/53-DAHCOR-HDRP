using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class RacketLayoutQuestion : MonoBehaviour
{
    [SerializeField] protected bool _Answered;
    private RacketLayoutQuestionController _Controller;
    private RacketLayoutExtraEffect[] _ExtraEffects;
    protected bool _Initialized = false;

    protected string _Answer;
    protected int _Cost;

    public void BaseInitialize(RacketLayoutQuestionController controller)
    {
        _Initialized = true;
        _Controller = controller;
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

        RacketLayoutSummaryController.Instance.UpdateSummary();

        _Controller.CheckIfAllQuestionsAreAnswered();

        RefreshUi(); // Fixes some visual bugs
    }
    public bool IsAnswered => _Answered;

    public int GetCost() => _Cost;
    public string GetAnswer() => _Answer;
    public void SetAnswerData(string answer, int cost)
    {
        _Answer = answer;
        _Cost = cost;
    }

    protected void RefreshUi()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(RefreshUiCoroutine());
        }
    }
    private IEnumerator RefreshUiCoroutine()
    {
        Canvas.ForceUpdateCanvases();
        ChangeLayoutGroupValues();
        Canvas.ForceUpdateCanvases();
        RebuildLayout();
        Canvas.ForceUpdateCanvases();
        ChangeLayoutGroupValues();
        Canvas.ForceUpdateCanvases();
        
        yield return new WaitForEndOfFrame();
        
        Canvas.ForceUpdateCanvases();
        ChangeLayoutGroupValues();
        Canvas.ForceUpdateCanvases();
        RebuildLayout();
    }

    private void RebuildLayout()
    {
        if (transform.parent.name == "Content")
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent);
        }
        else LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.parent.parent);
    }

    private void ChangeLayoutGroupValues()
    {
        var vLayoutGroup = GetComponent<VerticalLayoutGroup>();
        if (vLayoutGroup != null)
        {
            vLayoutGroup.spacing = vLayoutGroup.spacing == 0 ? 0.00001f : 0;
            vLayoutGroup.CalculateLayoutInputHorizontal();
            vLayoutGroup.CalculateLayoutInputVertical();
            vLayoutGroup.SetLayoutHorizontal();
            vLayoutGroup.SetLayoutVertical();
        }
        var hLayoutGroup = GetComponent<VerticalLayoutGroup>();
        if (hLayoutGroup != null)
        {
            hLayoutGroup.spacing = hLayoutGroup.spacing == 0 ? 0.00001f : 0;
            hLayoutGroup.CalculateLayoutInputHorizontal();
            hLayoutGroup.CalculateLayoutInputVertical();
            hLayoutGroup.SetLayoutHorizontal();
            hLayoutGroup.SetLayoutVertical();
        }
    }
}
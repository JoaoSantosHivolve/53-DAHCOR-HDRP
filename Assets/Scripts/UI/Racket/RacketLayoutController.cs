using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutController : MonoBehaviour
{
    private RacketLayoutButton[] _Buttons;
    private RacketViewController _ViewController;
    private RacketLayoutQuestionController[] _QuestionsController;
    private RacketLayoutExtraEffect[] _ExtraEffects;
    private Animator _Ui;

    private bool _QuestionsInitialized;
    private bool _ExtraEffectsInitialized;
    private bool _QuestionsUpdated;
    private bool _ExtraEffectsLateInitialized;


    private void Awake()
    {
        _Buttons = transform.GetComponentsInChildren<RacketLayoutButton>();
        _Ui = GameObject.Find("Canvas - UI").GetComponent<Animator>();
        _ViewController = GameObject.Find("[Racket Controller]").GetComponent<RacketViewController>();
        _QuestionsController = GameObject.FindObjectsOfType<RacketLayoutQuestionController>();
        _ExtraEffects = GameObject.FindObjectsOfType<RacketLayoutExtraEffect>();
    }

    private void Start()
    {
        _QuestionsInitialized = false;
        _ExtraEffectsInitialized = false;
        _QuestionsUpdated = false;
        _ExtraEffectsLateInitialized = false;

        // Set Button index
        for (int i = 0; i < _Buttons.Length; i++)
        {
            _Buttons[i].SetIndex(i);
            _Buttons[i].SetController(_ViewController);
        }

        // Make only first button interactable
        //for (int i = 0; i < _Buttons.Length; i++)
        //{
        //    _Buttons[i].SetInteractable(i == 0 || i == 1);
        //    _Buttons[i].SetAvailable(i == 0 || i == 1);
        //}
        foreach (var item in _Buttons)
        {
            item.SetInteractable(true);
            item.SetAvailable(true);
        }
    }

    public void OpenButton(int index)
    {
        for (int i = 0; i < _Buttons.Length; i++)
        {
            if (index == i) 
                _Buttons[i].OpenLayout();
            else 
                _Buttons[i].CloseLayout();
        }
    }

    public void SetupApp() => StartCoroutine(SetupComponents());

    private IEnumerator SetupComponents()
    {
        StartCoroutine(InitializeAllQuestions());
        while (!_QuestionsInitialized)
            yield return null;

        StartCoroutine(InitializeAllExtraEffects());
        while (!_ExtraEffectsInitialized)
            yield return null;

        StartCoroutine(UpdateQuestionsData());
        while (!_QuestionsUpdated)
            yield return null;

        StartCoroutine(InitializeAllExtraEffectsLate());
        while (!_ExtraEffectsLateInitialized)
            yield return null;

        _Ui.SetTrigger("StartFadeIn");
    }
    private IEnumerator InitializeAllQuestions()
    {
        foreach (var item in _QuestionsController)
        {
            item.InitializeQuestions();
            yield return null;
        }

        _QuestionsInitialized = true;
        Debug.Log("<color=yellow> Questions Initialized</color>");
    }
    private IEnumerator InitializeAllExtraEffects()
    {
        foreach (var item in _ExtraEffects)
        {
            item.Initialize();
            yield return null;
        }

        _ExtraEffectsInitialized = true;
        Debug.Log("<color=yellow> Extra Effects Initialized</color>");
    }
    private IEnumerator InitializeAllExtraEffectsLate()
    {
        foreach (var item in _ExtraEffects)
        {
            item.LateInitialize();
            yield return null;
        }

        _ExtraEffectsLateInitialized = true;
        Debug.Log("<color=yellow> Extra Effects Late Initialized</color>");
    }
    public IEnumerator UpdateQuestionsData()
    {
        foreach (var questionsController in _QuestionsController)
        {
            questionsController.UpdateData();
            yield return null;
        }

        _QuestionsUpdated = true;
        Debug.Log("<color=yellow> Questions Updated</color>");
    }
}
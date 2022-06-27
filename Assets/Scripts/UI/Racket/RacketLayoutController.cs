using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutController : MonoBehaviour
{
    private RacketLayoutButton[] _Buttons;
    private RacketViewController _ViewController;
    private RacketLayoutQuestionController[] _QuestionsController;
    private Animator _Ui;

    private void Awake()
    {
        _Buttons = transform.GetComponentsInChildren<RacketLayoutButton>();
        _Ui = GameObject.Find("Canvas - UI").GetComponent<Animator>();
        _ViewController = GameObject.Find("[Racket Controller]").GetComponent<RacketViewController>();
        _QuestionsController = GameObject.FindObjectsOfType<RacketLayoutQuestionController>();
    }

    private void Start()
    {
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
        yield return new WaitForSeconds(1f);

        InitializeAllQuestions();

        yield return new WaitForSeconds(1f);

        UpdateQuestionsData();

        _Ui.SetTrigger("StartFadeIn");
    }

    private void InitializeAllQuestions()
    {
        foreach (var item in _QuestionsController)
        {
            item.InitializeQuestions();
        }

        Debug.Log("<color=yellow> Questions Initialized</color>");
    }

    public void UpdateQuestionsData()
    {
        foreach (var questionsController in _QuestionsController)
        {
            questionsController.UpdateData();
        }

        Debug.Log("<color=yellow> Questions Updated</color>");
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
public class RacketLayoutButton : MonoBehaviour
{
    private Animator _Animator;
    private Button _Button;
    private RacketLayoutController _Controller;
    private RacketViewController _ViewController;

    private int _Index;
    private bool _Open;
    private bool Open
    {
        get { return _Open; }
        set
        {
            _Open = value;

            _Animator.SetBool("Open", value);
        }
    }
    private bool _Interactable;
    private bool Interactable
    {
        get { return _Interactable; }
        set
        {
            _Interactable = value;

            _Animator.SetBool("Interactable", value);
        }
    }
    private bool _Available;
    private bool Available
    {
        get { return _Available; }
        set
        {
            _Available = value;

            _Animator.SetBool("Available", value);
        }
    }
    private bool _Completed;
    private bool Completed
    {
        get { return _Completed; }
        set
        {
            _Completed = value;

            _Animator.SetBool("Completed", value);
        }
    }

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(OpenLayoutOnClick);
        _Controller = transform.parent.GetComponent<RacketLayoutController>();
    }

    private void OpenLayoutOnClick() => _Controller.OpenButton(_Index);
    public void SetIndex(int index)
    {
        _Index = index;
        // Change ui
        transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = (_Index+1) + "/8";
    }
    public void SetController(RacketViewController viewController) => _ViewController = viewController;


    public void OpenLayout()
    {
        Open = true;
        Interactable = false;

        // Change Racket View
        _ViewController.SetView(_Index);
    }
    public void CloseLayout()
    {
        Open = false;
        Interactable = Available;
    }
    
    public void SetCompleted() => Completed = true;
    public void SetUncomplete() => Completed = false;

    public void SetInteractable(bool state) => Interactable = state;
    public void SetAvailable(bool state) => Available = state;
}
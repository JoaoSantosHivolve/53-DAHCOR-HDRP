using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    private List<Image> _Backgrounds;
    private List<Button> _Buttons;
    [SerializeField] private int _CurrentBackgroundIndex;

    private void Awake()
    {
        _Backgrounds = new List<Image>();
        _Buttons = new List<Button>();
        _CurrentBackgroundIndex = 0;

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            _Backgrounds.Add(transform.GetChild(0).GetChild(i).GetComponent<Image>());
        }

        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            _Buttons.Add(transform.GetChild(1).GetChild(i).GetComponent<Button>());
        }
    }

    private void Start()
    {
        foreach (var item in _Backgrounds)
        {
            item.enabled = false;
        }

        SetBackground(0);
    }

    public void SetBackground(int index)
    {
        _Backgrounds[_CurrentBackgroundIndex].enabled = false;
        _CurrentBackgroundIndex = index;
        _Backgrounds[index].enabled = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    //private List<Image> _Backgrounds;
    private List<Button> _Buttons;
    [SerializeField] private int _CurrentBackgroundIndex;

    public Volume volume;
    public List<Cubemap> _Backgrounds;
    private HDRISky _Sky;

    private void Awake()
    {
        //_Backgrounds = new List<Image>();
        _Buttons = new List<Button>();
        _CurrentBackgroundIndex = 0;

        volume.profile.TryGet(out _Sky);

        //for (int i = 0; i < transform.GetChild(0).childCount; i++)
        //{
        //    _Backgrounds.Add(transform.GetChild(0).GetChild(i).GetComponent<Image>());
        //}

        for (int i = 0; i < transform.childCount; i++)
        {
            _Buttons.Add(transform.GetChild(i).GetComponent<Button>());
        }
    }

    private void Start()
    {
        SetBackground(0);
    }

    public void SetBackground(int index)
    {
        //_Backgrounds[_CurrentBackgroundIndex].enabled = false;
        _CurrentBackgroundIndex = index;
        //_Backgrounds[index].enabled = true;
        _Sky.hdriSky.value = _Backgrounds[index];
    }
}
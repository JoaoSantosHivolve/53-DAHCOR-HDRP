using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketLayoutQuestionDropdown : RacketLayoutQuestion
{
    [SerializeField] private RacketLayoutChoiceDropdown _Choice;

    private void Awake()
    {
        _Choice  = transform.Find("Dropdown").GetComponentInChildren<RacketLayoutChoiceDropdown>();
    }

    public override void Initialize()
    {
        _Choice.InitializeChoiceElement(this);
    }

    public override void OnReset()
    {
        _Answered = false;
    }

    public override void UpdateData()
    {
    }
}
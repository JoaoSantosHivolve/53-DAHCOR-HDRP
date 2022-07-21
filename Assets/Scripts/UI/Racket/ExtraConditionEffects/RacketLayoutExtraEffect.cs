using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RacketLayoutExtraEffect : MonoBehaviour
{
    protected RacketLayoutQuestion _BaseQuestion;

    private void Awake()
    {
        _BaseQuestion = GetComponent<RacketLayoutQuestion>();
    }

    public abstract void Initialize();
    public abstract void LateInitialize();
    public abstract void OnClickEffect();
}
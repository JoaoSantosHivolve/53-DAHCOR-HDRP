using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RacketLayoutExtraEffect : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void LateInitialize();
    public abstract void OnClickEffect();
}
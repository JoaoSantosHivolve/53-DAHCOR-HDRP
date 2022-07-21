using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineWithData
{
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator _Target;
    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this._Target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (_Target.MoveNext())
        {
            result = _Target.Current;
            yield return result;
        }
    }
}

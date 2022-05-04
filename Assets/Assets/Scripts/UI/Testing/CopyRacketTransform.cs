using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRacketTransform : MonoBehaviour
{
    private Transform _ReferenceRacket;

    private Vector3 RefPosition => _ReferenceRacket.position;
    private Vector3 RefRotation => _ReferenceRacket.eulerAngles;
    private void Awake()
    {
        _ReferenceRacket = GameObject.Find("Racket").transform;
    }

    private void Update()
    {
        transform.position = new Vector3(RefPosition.x, -RefPosition.y, RefPosition.z);
        transform.rotation = Quaternion.Euler(-RefRotation.x, RefRotation.y, -RefRotation.z);
    }
}
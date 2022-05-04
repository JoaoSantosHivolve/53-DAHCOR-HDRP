using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketViewController : MonoBehaviour
{
    private Transform _Camera;
    private Transform _Racket;
    private RacketInteractionController _InteractionController;

    [SerializeField] private bool _Transitioning;
    [SerializeField] private int _CurrentViewIndex;
    [SerializeField] private int _LastViewIndex;
    [SerializeField] [Range(1f, 10f)] private float _MoveSpeed = 1f;
    [SerializeField] [Range(100f, 300f)] private float _RotationSpeed = 100f;

    [SerializeField] private List<ViewInfo> _ViewInfo;
    private Vector3 TargetCameraPosition => _ViewInfo[_CurrentViewIndex].cameraPosition;
    private Quaternion TargetCameraRotation => _ViewInfo[_CurrentViewIndex].cameraRotation;
    private Vector3 TargetRacketPosition => _ViewInfo[_CurrentViewIndex].racketPosition;
    private Quaternion TargetRacketRotation => _ViewInfo[_CurrentViewIndex].racketRotation;

    private void Awake()
    {
        _Camera = transform.GetChild(0);
        _Racket = transform.GetChild(1);
        _InteractionController = transform.GetChild(2).GetComponent<RacketInteractionController>();
    }

    private void Start()
    {
        SetView(0);
        _Camera.localPosition = TargetCameraPosition;
        _Camera.localRotation = TargetCameraRotation;
        _Racket.localPosition = TargetRacketPosition;
        _Racket.localRotation = TargetRacketRotation;

        _InteractionController.Initialize(this, _Racket);
    }
    private void Update()
    {
        if (_LastViewIndex == _CurrentViewIndex)
            return;

        var transitionComplete = true;

        // Camera Position
        if(Vector3.Distance(_Camera.localPosition, TargetCameraPosition) > 0f)
        {
            transitionComplete = false;
            _Camera.localPosition = Vector3.MoveTowards(_Camera.localPosition, TargetCameraPosition, _MoveSpeed * Time.deltaTime);
        }
        // Camera Rotation
        if (_Camera.localRotation != TargetCameraRotation)
        {
            transitionComplete = false;
            _Camera.localRotation = Quaternion.RotateTowards(_Camera.localRotation, TargetCameraRotation, _RotationSpeed * Time.deltaTime);
        }
        // Racket Position
        if (Vector3.Distance(_Racket.localPosition, TargetRacketPosition) > 0f)
        {
            transitionComplete = false;
            _Racket.localPosition = Vector3.MoveTowards(_Racket.localPosition, TargetRacketPosition, _MoveSpeed * Time.deltaTime);
        }
        // Racket Rotation
        if (_Racket.localRotation != TargetRacketRotation)
        {
            transitionComplete = false;
            _Racket.localRotation = Quaternion.RotateTowards(_Racket.localRotation, TargetRacketRotation, _RotationSpeed * Time.deltaTime);
        }
        if (transitionComplete)
        {
            _Transitioning = false;
            _LastViewIndex = _CurrentViewIndex;
        }
    }

    public void SetView(int index)
    {
        if (index >= 0 && index < _ViewInfo.Count && index != _CurrentViewIndex)
        {
            _CurrentViewIndex = index;
            _Transitioning = true;
            //var info = _ViewInfo[index];
            //_Camera.localPosition = info.cameraPosition;
            //_Camera.localEulerAngles = info.cameraRotation;
            //_Racket.localPosition = info.racketPosition;
            //_Racket.localEulerAngles = info.racketRotation;
        }
    }

    public bool GetTransitioningState() => _Transitioning;
}

[Serializable]
public struct ViewInfo
{
    public string name;
    public Vector3 cameraPosition;
    public Quaternion cameraRotation;
    public Vector3 racketPosition;
    public Quaternion racketRotation;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RacketInteractionController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool _Interacting;
    [SerializeField] [Range(10f, 100f)] private float _RotationSpeed;
    [SerializeField] private float _PointerDistance;
    private float _LastMousePosition;
    private RotatingDirection _Direction;

    private Transform _Racket;
    private RacketViewController _ViewController;
    private Rigidbody _Rigidbody => _Racket.GetComponent<Rigidbody>();
    
    private void Update()
    {
        if (_Interacting)
        {
            var pointerPosition = Input.mousePosition.x;
            _PointerDistance = Mathf.Abs(pointerPosition - _LastMousePosition);

            if(pointerPosition == _LastMousePosition)
            {
                _Direction = RotatingDirection.None;
                return;
            }
            else if(pointerPosition > _LastMousePosition)
            {
                _Direction = RotatingDirection.Left;
                _Racket.RotateAround(_Racket.position, _Racket.up, (-_RotationSpeed * _PointerDistance) * Time.deltaTime);
            }
            else
            {
                _Direction = RotatingDirection.Right;
                _Racket.RotateAround(_Racket.position, _Racket.up, (_RotationSpeed * _PointerDistance) * Time.deltaTime);
            }

            _LastMousePosition = pointerPosition;
        }
    }

    public void Initialize(RacketViewController controller, Transform racket)
    {
        _ViewController = controller;
        _Racket = racket;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_ViewController.GetTransitioningState() == false)
        {
            _Interacting = true;

            _LastMousePosition = Input.mousePosition.x;

            // Reset kinematic rotation
            _Rigidbody.angularVelocity = Vector3.zero;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_ViewController.GetTransitioningState() == false)
        {
            _Interacting = false;

            var direction = _Direction == RotatingDirection.Right ? Vector3.up : Vector3.down;

            _Rigidbody.AddTorque(direction * (_PointerDistance / 3f), ForceMode.Impulse);
        }
    }
}

public enum RotatingDirection
{
    None,
    Left,
    Right
}
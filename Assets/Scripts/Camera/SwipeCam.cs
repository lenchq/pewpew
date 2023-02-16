using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Models;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
#nullable enable
public class SwipeCam : MonoBehaviour
{
    private CinemachineInputProvider? _input;

    private CinemachineFreeLook? _camera;

    // private float limiterMaxX;
    // private float limiterMaxY;
    // private float limiterMinX;
    // private float limiterMinY;
    
    private bool _dragging;
    private Vector3 _cameraPosition;
    private Vector2 _hitPosition;
    private Vector2 _currentPosition;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Collider2D? cameraLimiter;

    private Vector3 _targetPosition;
    //private TouchControls _touchControls;
    private MouseDetector _mouseDetector;
    private BoxCollider2D? _cameraBounds;

    void Awake()
    {
        // var bounds = cameraLimiter!.bounds;
        // limiterMaxX = bounds.max.x;
        // limiterMaxY = bounds.max.y;
        // limiterMinX = bounds.min.x;
        // limiterMinY = bounds.min.y;

        _input = GetComponent<CinemachineInputProvider>();
        _camera = GetComponent<CinemachineFreeLook>();
        //_touchControls = new TouchControls();
        _cameraBounds = GetComponent<BoxCollider2D>();
        _mouseDetector = GameObject.Find("vovo").GetComponent<MouseDetector>();
    }
    void Update()
    {
        CalculateBounds();
    }

    void CalculateBounds()
    {
        var height = 2f * _camera!.m_Lens.OrthographicSize;
        var width = height * _camera!.m_Lens.Aspect;
        _cameraBounds!.size = new(width, height);
    }

    // private void LateUpdate()
    // {
    //     var mouse = Mouse.current;
    //     if (mouse.leftButton.isPressed)
    //     {
    //         var pos = mouse.position.ReadValue();
    //         var pp = Camera.main.ScreenToWorldPoint(pos);
    //         pp.z = -10;
    //         _camera.ForceCameraPosition(pp, _camera.transform.rotation);
    //     }
    // }

    private void LateUpdate()
    {
        var mouse = Mouse.current;
        
        // Touchscreen? touch = Touchscreen.current;
        
        if (mouse.leftButton.wasPressedThisFrame && !_mouseDetector.mouseOver)
        {
            
            _hitPosition = mouse.position.ReadValue();
            //Debug.Log(_hitPosition);
            _cameraPosition = _camera!.transform.position;
        }
        // else if (touch != null && touch.primaryTouch.press.wasPressedThisFrame)
        // {
        //     _hitPosition = touch.position.ReadValue();
        //     //Debug.Log("Hit");
        //     _cameraPosition = _camera!.transform.position;
        // }
        if (mouse.leftButton.isPressed && !_mouseDetector.mouseOver)
        {
            
            _currentPosition = mouse.position.ReadValue();
            HandleDrag();
            _dragging = true;
        }
        // else if (touch != null && touch.primaryTouch.isInProgress)
        // {
        //     //Debug.Log("Pressed");
        //     _currentPosition = touch.position.ReadValue();
        //     HandleDrag();
        //     _dragging = true;
        // }
        
        
        if (_dragging)
        {
            
            var position = _camera!.transform.position;
            //var destination = Vector3.Lerp(position, _targetPosition, Time.deltaTime * speed);
            var velocity = Vector3.zero;
            var destination = Vector3.Slerp(position, _targetPosition,
                /*ref velocity,*/ Time.deltaTime * speed);


            var camBounds = _cameraBounds!.bounds;
            var limBounds = cameraLimiter!.bounds;
            var limBoundsMinX = limBounds.min.x + (camBounds.size.x / 2f);
            var limBoundsMaxX = limBounds.max.x - (camBounds.size.x / 2f);
            
            var limBoundsMinY = limBounds.min.y + (camBounds.size.y / 2f);
            var limBoundsMaxY = limBounds.max.y - (camBounds.size.y / 2f);
            
            
            destination.x = (float)Math.Round(Mathf.Clamp(destination.x, limBoundsMinX, limBoundsMaxX), 5);
            destination.y = (float)Math.Round(Mathf.Clamp(destination.y, limBoundsMinY, limBoundsMaxY), 5);
            destination.z = -10;
            
            // else if (destination.x <= minX)
            // {
            //     destination.x = minX;
            // }
            //
            // if (destination.y >= maxY)
            // {
            //     destination.y = maxY;
            // }
            // else if (destination.y <= minY)
            // {
            //     destination.y = minY;
            // 
            
            _camera.ForceCameraPosition(destination, _camera.transform.rotation);
            
            // if(touch != null && _camera.transform.position == _targetPosition && ( mouse.leftButton.wasReleasedThisFrame ||
            //        touch.primaryTouch.ReadValue().isNoneEndedOrCanceled))    //reached?
            // { 
            //     
            //     // stop moving
            //     _dragging = false;
            //     //Debug.Log("stop drag");
            // }
        }
    
        
        
    }

    private void HandleDrag()
    {
        // From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
        // with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
        //_currentPosition.z = _hitPosition.z = _cameraPosition.y;

        // Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
        // anyways. 
        Vector3 currPosPoint = Camera.main.ScreenToWorldPoint(_currentPosition);
        Vector3 hitPositionPoint = Camera.main.ScreenToWorldPoint(_hitPosition);
        
        Vector3 direction = (currPosPoint - hitPositionPoint);

        // Invert direction to that terrain appears to move with the mouse.
        direction = -direction;
        
        _targetPosition = _cameraPosition + direction;
    }
}

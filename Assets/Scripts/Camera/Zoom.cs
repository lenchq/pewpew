using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Zoom : MonoBehaviour
{
    // [Range(10f, 30f)]
    // [SerializeField]
    // private float zoomMax = 10.0f;
    //
    // [Range(3f, 10f)]
    // [SerializeField]
    // private float zoomMin = 3.0f;

    [SerializeField]
    private float zoomValue = 1f;

    private CinemachineVirtualCamera _camera;
    private float _currentZoom = 3f;

    private GameControls _controls;

    private bool _zooming;
    [SerializeField]
    private float zoomTime;

    // Start is called before the first frame update
    void Start()
    {
        _controls = new GameControls();
        _camera = GetComponent<CinemachineVirtualCamera>();
        _controls.Mouse.Zoom.performed += HandleZoom;
        
    }
    

    private void HandleZoom(InputAction.CallbackContext scroll)
    {
        //TODO probably use scroll delta
        float scrollDelta = scroll.ReadValue<float>();
        
        if (scrollDelta > 1f)
            _currentZoom += zoomValue;
        else if (scrollDelta < 1f)
            _currentZoom -= zoomValue;
        _zooming = true;

    }

    private void LateUpdate()
    {
        if (_zooming)
        {
            float dest = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _currentZoom, zoomTime);
            _camera.m_Lens.OrthographicSize = dest;
            if (_camera.m_Lens.OrthographicSize == _currentZoom)
            {
                _zooming = false;
            }
        }
    }
}

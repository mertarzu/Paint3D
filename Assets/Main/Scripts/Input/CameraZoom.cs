using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Action OnZoomChanged;
    [SerializeField] Camera _camera;
    [SerializeField] GamePlayScreen _gamePlayScreen;
    [SerializeField] float ZoomSpeed = 4.0f;
    [SerializeField] float ZoomMinBound = 30f;
    [SerializeField] float ZoomMaxBound = 70f;

    bool _isMinusActive;
    bool _isPlusActive;
    public void Initialize()
    {
        _isPlusActive = false;
        _isMinusActive = false;
        _gamePlayScreen.OnZoomMinusPressed += OnZoomMinusPressed;
        _gamePlayScreen.OnZoomPlusPressed += OnZoomPlusPressed;
    }

    private void OnZoomPlusPressed(bool value)
    {
        _isPlusActive = value;
    }

    private void OnZoomMinusPressed(bool value)
    {
        _isMinusActive = value;
    }

    void LateUpdate()
    {
        if (_isMinusActive) 
        {
            Zoom(1f, ZoomSpeed);
            
        }
        
        if (_isPlusActive)
        {
            Zoom(-1f, ZoomSpeed);
            
        }         
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {   
        _camera.fieldOfView += deltaMagnitudeDiff * speed;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, ZoomMinBound, ZoomMaxBound);
        OnZoomChanged();
    }
}

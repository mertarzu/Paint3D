using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using System;

public class CameraController : MonoBehaviour,ISaveable
{
    
    [SerializeField] CameraZoom _cameraZoom;
    [SerializeField] SavingSystem _savingSystem;
    [SerializeField] Camera _camera;
    [SerializeField] Background _background;
    string _saveFile;
    public void Initialize()
    {
        _cameraZoom.OnZoomChanged += ZoomStateChanged;
        _background.Initialize();
    }
    public void ZoomStateChanged()
    {
        _savingSystem.Save(_saveFile);
    }

    public object CaptureState()
    {
        float fov = _camera.fieldOfView;
        return fov;
    }

    public void RestoreState(object state)
    {
        if (state != null)
        {
            float fov = (float)state;
            _camera.fieldOfView = fov;
        }
    }

    public void SetSaveFile(string message)
    {
        _saveFile = message;
        _background.SetSaveFile(message);
    }
}

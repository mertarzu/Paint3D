using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
   
    [SerializeField] Rotater _rotater;
    [SerializeField] CameraZoom _cameraZoom;
    [SerializeField] ScreenShot _screenShot;
    public void Initialize()
    {
      
        _rotater.Initialize();
        _cameraZoom.Initialize();
        _screenShot.Initialize();
    }
}

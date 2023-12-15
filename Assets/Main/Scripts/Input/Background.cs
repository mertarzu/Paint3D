using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using System;

public class Background : MonoBehaviour,ISaveable
{
    [SerializeField] SavingSystem _savingSystem;
    [SerializeField] Camera _camera;
    string _message;
    string _saveFile;

    public void Initialize()
    {
      
    }

    public void SetSaveFile(string saveFile)
    {
        _saveFile = saveFile;
    }
    public void BackgroundChanged()
    {
        _savingSystem.Save(_saveFile);
    }
    public object CaptureState()
    {
        return _message;
    }

    public void RestoreState(object state)
    {
        if (state != null)
        {
            string message = state.ToString();
            SetBackgroundColor(message);
        }
    }
   
    public void SetBackgroundColor(String message)
    {
        _message = message;
        _camera.backgroundColor = message.ToColor();
        BackgroundChanged();
    }
    private void Update()
    {
        //CheatUpdate();
    }

    private void CheatUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetBackgroundColor("#FFFFFF");
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using System;

public class MovementSaver : MonoBehaviour,ISaveable
{
    [SerializeField] SavingSystem _savingSystem;
    [SerializeField] Rotater _rotater;
    string _saveFile;
    public void RotationChanged()
    {
        _savingSystem.Save(_saveFile);
    }
    public object CaptureState()
    {
        var transforms = new SerializableVector3[2];
        transforms[0] = new SerializableVector3(transform.position);
        transforms[1] = new SerializableVector3(transform.rotation.eulerAngles);

        return transforms;
    }

    public void RestoreState(object state)
    {
        if (state != null)
        {
            var transforms = (SerializableVector3[])state;
            transform.position = transforms[0].ToVector();
            transform.rotation = Quaternion.Euler(transforms[1].ToVector());
        }
    }
    public void Initialize()
    {
        _rotater.OnRotateChanged += RotationChanged;
    }

    public void SetSaveFile(string saveFile)
    {
        _saveFile = saveFile;
    }
}

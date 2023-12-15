using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
public class Rotater : MonoBehaviour
{
    public Action OnRotateChanged;
    [SerializeField] Model _player;
    [SerializeField] float _rotateSpeed = 10f;
    [SerializeField] GamePlayScreen _gamePlayScreen;

    bool _isRightActive;
    bool _isLeftActive;
    bool _isDownActive;
    bool _isUpActive;
    public void Initialize()
    {
        _isRightActive = false;
        _isDownActive = false;
        _isLeftActive = false;
        _isUpActive = false;
        _gamePlayScreen.OnRotateDownPressed += OnRotateDownPressed;
        _gamePlayScreen.OnRotateRightPressed += OnRotateRightPressed;
        _gamePlayScreen.OnRotateUpPressed += OnRotateUpPressed;
        _gamePlayScreen.OnRotateLeftPressed += OnRotateLeftPressed;
    }

    private void OnRotateLeftPressed(bool value)
    {
        _isLeftActive = value;
    }

    private void OnRotateUpPressed(bool value)
    {
        _isUpActive = value;
    }

    private void OnRotateRightPressed(bool value)
    {
        _isRightActive = value;
    }

    private void OnRotateDownPressed(bool value)
    {
        _isDownActive = value;
    }
    void LateUpdate()
    {
        if (_isLeftActive)
        {
            _player.gameObject.transform.eulerAngles += Vector3.up * _rotateSpeed * Time.deltaTime;
            OnRotateChanged();
        }
        if (_isRightActive)
        {
            _player.gameObject.transform.eulerAngles += Vector3.down * _rotateSpeed * Time.deltaTime;
            OnRotateChanged();
        }
        if (_isDownActive)
        {
            _player.gameObject.transform.eulerAngles += Vector3.right * _rotateSpeed * Time.deltaTime;
            OnRotateChanged();
        }
        if (_isUpActive)
        {
            _player.gameObject.transform.eulerAngles += Vector3.left * _rotateSpeed * Time.deltaTime;
            OnRotateChanged();
        }
     
    
    }
  
    
}

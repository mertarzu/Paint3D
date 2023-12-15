using Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  
    [SerializeField] ScreenManager _screenManager;
    [SerializeField] PlayerController _playerController;
    [SerializeField] InputController _inputController;
    [SerializeField] SavingSystem _savingSystem;
    [SerializeField] CameraController _cameraController;
    private GameState _gameState;
    enum GameState
    {
        Gameplay
    }

    private void Awake()
    {
      
        Initialize();
    }
    
    public void Initialize()
    {
        LoadFile("save");
        _screenManager.Initialize();
        _playerController.Initialize();
        _inputController.Initialize();
        _cameraController.Initialize();
        StartGame();
    }

    public void StartGame()
    {
        _playerController.StartGame();
        SetGamePlayState();
    }

    void SetGamePlayState()
    {
        _screenManager.Hide((int)_gameState);
        _gameState = GameState.Gameplay;    
        _screenManager.Show((int)_gameState);
    }

    public void SaveFile(string message)
    {
        _savingSystem.Save(message);
    }
    public void LoadFile(string message)
    {
        _savingSystem.Load(message);
        _playerController.SetSaveFile(message);
        _cameraController.SetSaveFile(message);
        SaveFile(message);
    }
    public void DeleteFile(string message)
    {
        _savingSystem.Delete(message);
    }

}

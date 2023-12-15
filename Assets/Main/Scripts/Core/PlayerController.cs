using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] Player _player;

 
    public void Initialize()
    {    
        _player.Initialize();       
    }
   
    public void StartGame()
    {       
        _player.StartGame();       
    }

    public void GameOver()
    {
        _player.GameOver();

    }

    public void SetSaveFile(string message)
    {
        _player.SetSaveFile(message);
    }
}

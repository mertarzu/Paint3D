using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class Player : MonoBehaviour
{
    public Action<bool> OnUndo;
 
    [SerializeField] GamePlayScreen _gamePlayScreen;
    [SerializeField] Model _model;
    [SerializeField] MovementSaver _movementSaver;
    [SerializeField] List<TexturePaint> _texturePaints;
    List<string> _canUndoBoyParts = new List<string>();
    
 
    public void Initialize()
    {
       
        _gamePlayScreen.OnBackPressed += OnBackPressed;
        _model.Initialize();
        _movementSaver.Initialize();
        foreach (TexturePaint texturePaint in _texturePaints)
        {
            texturePaint.Initialize();
            texturePaint.OnCanUndo += OnCanUndo;
            texturePaint.OnCanClear += OnCanClear;
        }
    }
    public void SetSaveFile(string saveFile)
    {
       
        _movementSaver.SetSaveFile(saveFile);
        foreach (var texturePaint in _texturePaints)
        {
            texturePaint.SetSaveFile(saveFile);
        }
    }
    public void StartGame()
    {
       
        gameObject.SetActive(true);


        foreach (TexturePaint texturePaint in _texturePaints)
        {
            texturePaint.StartGame();

        }
    }
    public void GameOver()
    {
        
    }

    
    private void OnCanClear(bool value)
    {
        _gamePlayScreen.UpdateClearButtonInteractable(value);
    }
    public void TextureClear()
    {
        foreach (TexturePaint texturePaint in _texturePaints)
        {
            texturePaint.ClearTexture();
        }
        OnCanClear(false);
    }
    private void OnBackPressed(bool value)
    {

        if (!value) return;
        if (_canUndoBoyParts.Count <= 0) return;

        int index = _canUndoBoyParts.Count - 1;
        String bodyPart = _canUndoBoyParts[index];
        String bodyPart2 = _canUndoBoyParts[index - 1];
        bool isDualBody = false;
        if (bodyPart2 == "BreastRight" || bodyPart2 == "BreastLeft")
        {
            isDualBody = true;
        }
            foreach (TexturePaint texturePaint in _texturePaints)
        {
            if (texturePaint.BodyPart == bodyPart) texturePaint.CanUndo();                
            else if(isDualBody)
            {
                if (texturePaint.BodyPart == bodyPart2) texturePaint.CanUndo();
            } 
        }
        _canUndoBoyParts.RemoveAt(index);
        if(isDualBody) _canUndoBoyParts.RemoveAt(index-1);
        if (_canUndoBoyParts.Count == 0)
        {
            if (OnUndo != null)
            {
                OnUndo(false);
                _gamePlayScreen.UpdateClearButtonInteractable(false);
            }
        }
    }
  

    private void OnCanUndo(string bodyPart)
    {
        _canUndoBoyParts.Add(bodyPart);
        if (OnUndo != null) OnUndo(true);
    }
   
}

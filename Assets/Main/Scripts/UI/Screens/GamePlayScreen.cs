using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScreen : UIScreen
{
    public Action OnHomePressed;
    public Action OnScreenShotPressed;
    public Action<bool> OnBackPressed;
    public Action<bool> OnClearPressed;
    public Action<bool> OnZoomMinusPressed;
    public Action<bool> OnZoomPlusPressed;
    public Action<bool> OnRotateLeftPressed;
    public Action<bool> OnRotateRightPressed;
    public Action<bool> OnRotateUpPressed;
    public Action<bool> OnRotateDownPressed;
    public Action<bool> OnClear;
    [SerializeField] Button _backButton;
    [SerializeField] Button _clearButton;
    [SerializeField] Player _player;
    public override void Hide()
    {
        gameObject.SetActive(false);
       
    }

    public override void Initialize()
    {
        Hide();
        _player.OnUndo += OnUndo;

    }

    public override void Show()
    {
        gameObject.SetActive(true);     
    }

    public void HomePressed()
    {
        Hide();
        OnHomePressed();
    }
    public void ScreenShotPressed()
    {
        OnScreenShotPressed();
    }
    public void ZoomMinusPressed(bool value)
    {
        OnZoomMinusPressed(value);
    }
    public void ZoomPlusPressed(bool value)
    {
        OnZoomPlusPressed(value);
    }

    public void RotateLeftPressed(bool value)
    {
        OnRotateLeftPressed(value);
    }
    public void RotateRightPressed(bool value)
    {
        OnRotateRightPressed(value);
    }
    public void RotateUpPressed(bool value)
    {
        OnRotateUpPressed(value);
    }
    public void RotateDownPressed(bool value)
    {
        OnRotateDownPressed(value);
    }
    public void BackPressed()
    {
        OnBackPressed(true);  
    }
    public void ClearPressed()
    {
        OnClearPressed(true);
        OnBackPressed(false);
        UpdateBackButtonInteractable(false);
        UpdateClearButtonInteractable(false);
    }
    public void UpdateBackButtonInteractable(bool value)
    {
        _backButton.interactable = value;
    }
    public void UpdateClearButtonInteractable(bool value)
    {
        _clearButton.interactable = value;
    }
    private void OnUndo(bool value)
    {
        UpdateBackButtonInteractable(value);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] List<UIScreen> _uiScreens;
    enum Screen
    {
        Gameplay
    }

    public void Initialize()
    {
        foreach (var uiScreen in _uiScreens) uiScreen.Initialize();
        
      
       
        GamePlayScreen gamePlayScreen = (GamePlayScreen)_uiScreens[(int)Screen.Gameplay];
        
    }

   

    public void Show(int screen)
    {
        _uiScreens[screen].Show();
    }

    public void Hide(int screen)
    {
        _uiScreens[screen].Hide();
    }

}

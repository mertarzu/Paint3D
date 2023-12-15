using System;
using System.Collections.Generic;
using UnityEngine;
using Saving;
public class TexturePaint : MonoBehaviour,ISaveable
{
    public String BodyPart => _bodyPart;
    public Action<string> OnCanUndo;
    public Action<bool> OnCanClear;
    [SerializeField] Camera _cam;
    [SerializeField] GamePlayScreen _gamePlayScreen; 
    [SerializeField] LayerMask _layerMask;  
    [SerializeField] String _bodyPart;
    [SerializeField] Transform _decalContainer;
    [SerializeField] SavingSystem _savingSystem;
    [SerializeField] PoolManager _poolManager;

    List<Decal> _decals = new List<Decal>();
    RaycastHit _hit;
    Vector3 _position;

    bool _isActive;
    bool _isPainting;  
    string _brush;
    string _saveFile;

    public object CaptureState()
    {
        var positions = new SerializableVector3[_decals.Count];
        for (int i = 0; i < _decals.Count; i++)
        {        
            positions[i] =new SerializableVector3(_decals[i].gameObject.transform.position);
        }
        return positions;
    }

    public void RestoreState(object state)
    {
        if (state != null)
        {
            var positions = (SerializableVector3[])state;
            for (int i = 0; i < positions.Length; i++)
            {
                for (int j = 0; j < _poolManager.GetCount(); j++)
                {
                    Decal decal = (Decal)_poolManager.GetItemByIndex(j);
                    if (decal != null)
                    {
                        decal.SetInitialParent(decal.transform.parent);
                        decal.SetUp(positions[i].ToVector(), Quaternion.FromToRotation(-Vector3.forward, _hit.normal), _decalContainer);
                        decal.SetActive();
                        _decals.Add(decal);
                    }
                }            
            }
        }
    }
    public void Initialize()
    {
        _gamePlayScreen.OnClearPressed += OnClearPressed;
        _position = Vector3.zero;
        _brush = "TestDecal";
        ChangeBrush(_brush);
    }
   
    public void SetSaveFile(string saveFile)
    {
        _saveFile = saveFile;
    }

    public void ChangeBrush(string brush)
    {
        _brush = brush;
    }
    public void StartGame()
    {
        _isActive = true;
        gameObject.SetActive(true);
       

    }
    public void ClearTexture()
    {
        OnClearPressed(true);
    }
    private void OnClearPressed(bool value)
    {
        if (!value) return;
      
        foreach (Decal decal in _decals)
        {
            decal.Dismiss();
        }
        _decals.Clear();
        _savingSystem.Save(_saveFile);
        if (OnCanUndo != null)
        {
            OnCanUndo(_bodyPart);
        }
        _isActive = true;
    }

    public void CanUndo()
    {
        if (_decals.Count == 0) return;
        _decals[_decals.Count - 1].Dismiss();
        _decals.RemoveAt(_decals.Count - 1);
        _savingSystem.Save(_saveFile);
        _isActive = true;
    }

    void Update()
    {
        
        if (_isActive)
        {
            if(_decals.Count > 0)
            {
                if (OnCanClear != null)
                {
                    OnCanClear(true);
                }
            }
            else
            {
                if (OnCanClear != null)
                {
                    OnCanClear(false);
                }
            }

            if (Input.GetMouseButton(0))
            {

                if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity, _layerMask) )
                {
             
                    if (Mathf.Abs(Vector3.Distance(_hit.point, _position)) <= Mathf.Epsilon) return;
                    _position = _hit.point;
                    Decal decal =(Decal)_poolManager.GetItemByName(_brush);
                    if (decal != null)
                    {
                        decal.SetInitialParent(decal.transform.parent);
                        decal.SetUp(_hit.point, Quaternion.FromToRotation(-Vector3.forward, _hit.normal), _decalContainer);
                        decal.SetActive();
                        _decals.Add(decal);
                    }
                                   
                    if (OnCanUndo != null)
                    {
                        OnCanUndo(_bodyPart);
                    }
                  
                    _isPainting = true;
                }
            }
            if (Input.GetMouseButtonUp(0) && _isPainting)
            {
                if (_bodyPart == "Main")
                {
                    _savingSystem.Save(_saveFile);
                }
                _isPainting = false;
            }
        }

    }
    
}


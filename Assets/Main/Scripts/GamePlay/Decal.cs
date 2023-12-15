using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decal : PooledObject
{
    bool _isTaken;
    public override bool IsPooledObjectTaken => _isTaken;
    Transform _parent;
    public override void Dismiss()
    {
        gameObject.SetActive(false);
        _isTaken = false;
        transform.parent = _parent;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public override void SetActive()
    {
        gameObject.SetActive(true);
        _isTaken = true;
    }

    public override void SetUp(Vector3 pos, Quaternion rot, Transform parent)
    {
        transform.position = pos;
        transform.rotation = rot;
        transform.parent = parent;
    }
    public void SetInitialParent(Transform parent)
    {
        _parent = parent;
    }

  
}

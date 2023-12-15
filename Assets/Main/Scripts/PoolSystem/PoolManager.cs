using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] List<ObjectPooler> _objectpoolers = new List<ObjectPooler>();
  
    public ObjectPooler this[int i] => _objectpoolers[i];
    
    public PooledObject GetItemByIndex(int index)
    {
        return _objectpoolers[index].GetPooledObject();
    }
    public PooledObject GetItemByName(string poolName)
    {
        return _objectpoolers.Where(i => i.GetPoolName() == poolName).FirstOrDefault().GetPooledObject();
    }

    public int GetCount()
    {
        return _objectpoolers.Count;
    }
    public void ReleaseAll()
    {
        
        for(int i = 0; i < _objectpoolers.Count; i++)
        {
            for(int j = 0; j < _objectpoolers[i].PooledObjects.Count; j++)
            {
                _objectpoolers[i].PooledObjects[j].Dismiss();
            }
        }       
    }
}

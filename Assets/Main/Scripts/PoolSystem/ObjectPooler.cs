using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Saving;
public class ObjectPooler : MonoBehaviour
{
    public List<PooledObject> PooledObjects = new List<PooledObject>();
    [SerializeField] PooledObject _prefab;
    [SerializeField] int _amountToPool;
    [SerializeField] int _additionAmountToPool;
    [SerializeField] string _poolName; 
    [SerializeField] SavingSystem _savingSystem;
    int _tempAmountToPool;
    private void Awake()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            PooledObjects.Add(Create());
        }
    }
    
    public PooledObject Create()
    {
        PooledObject tempGameObject = GameObject.Instantiate(_prefab);
        tempGameObject.gameObject.SetActive(false);
        tempGameObject.gameObject.transform.SetParent(transform, true);
        return tempGameObject;
    }

    public PooledObject GetPooledObject()
    {
        PooledObject result = PooledObjects.Where(i => i.IsPooledObjectTaken == false).FirstOrDefault();
        if (result == null && _tempAmountToPool < _additionAmountToPool)
        {
            AddPooledObject();
            result = PooledObjects.Where(i => i.IsPooledObjectTaken == false).FirstOrDefault();
            ++_tempAmountToPool;
        }
        return result;
    }

    public void AddPooledObject()
    {
        PooledObjects.Add(Create());
    }

    public  string GetPoolName()
    {
        return _poolName;
    }
}

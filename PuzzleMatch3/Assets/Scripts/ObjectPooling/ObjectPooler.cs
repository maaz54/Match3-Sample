using Puzzle.Match.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour, IObjectPooler
{
    // List of objects that can be pooled
    private List<IPoolableObject> poolableObjects = new();

    // Pool an object and return it
    public T Pool<T>(IPoolableObject _object, Transform parent = null) where T : MonoBehaviour
    {
        T obj = null;
        IPoolableObject poolableObject = poolableObjects.Find(i => i.ObjectID == _object.ObjectID);
        if (poolableObject == null)
        {
            obj = Instantiate(_object.Transform.gameObject).GetComponent<T>();
        }
        else
        {
            obj = poolableObject.Transform.gameObject.GetComponent<T>();
            poolableObjects.Remove(poolableObject);
        }

        if (obj != null)
        {
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(parent);
        }

        return obj;
    }

    // Remove an object from the pool
    public void Remove<T>(T _object) where T : IPoolableObject
    {
        if (_object is IPoolableObject poolObject)
        {
            poolObject.Transform.gameObject.SetActive(false);
            poolObject.Transform.gameObject.transform.parent = transform;
            poolableObjects.Add(poolObject);
        }
    }
}

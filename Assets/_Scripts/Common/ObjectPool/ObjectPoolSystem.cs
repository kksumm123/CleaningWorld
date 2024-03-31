using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSystem
{
    private int LAST_INDEX => _objectPool.Count - 1;

    private RecycleObject _recycleObjectPrefab;
    private RecycleObject _tempObject;
    private List<RecycleObject> _objectPool = new List<RecycleObject>();
    private Transform _parent;

    private int _defaultPoolSize;
    private Quaternion _originalRotation;

    public ObjectPoolSystem(RecycleObject recycleObjectPrefab, int defaultPoolSize, Transform parent)
    {
        if (defaultPoolSize <= 0)
        {
            defaultPoolSize = 1;
        }

        _recycleObjectPrefab = recycleObjectPrefab;
        _defaultPoolSize = defaultPoolSize;
        _parent = parent;
        _originalRotation = recycleObjectPrefab.transform.localRotation;
    }

    private void CreateObject()
    {
        for (int i = 0; i < _defaultPoolSize; i++)
        {
            _tempObject = MonoBehaviour.Instantiate(_recycleObjectPrefab, _parent);
            _tempObject.InitializedByObjectPoolSystem(Restore);
            _tempObject.gameObject.SetActive(false);
            _objectPool.Add(_tempObject);
        }
    }

    public RecycleObject Get()
    {
        if (_objectPool.Count <= 0)
        {
            CreateObject();
        }

        _tempObject = _objectPool[LAST_INDEX];
        _objectPool.RemoveAt(LAST_INDEX);
        _tempObject.gameObject.SetActive(true);
        return _tempObject;
    }

    public void Restore(RecycleObject recycleObject)
    {
        if (_objectPool.Contains(recycleObject)) return;

        recycleObject.gameObject.SetActive(false);
        recycleObject.transform.localScale = Vector3.one;
        recycleObject.transform.localRotation = _originalRotation;
        recycleObject.transform.SetParent(_parent);
        _objectPool.Add(recycleObject);
    }
}

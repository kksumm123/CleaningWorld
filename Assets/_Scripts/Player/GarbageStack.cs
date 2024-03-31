using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

class GarbageStack<T> where T : GarbageObject
{
    private StringBuilder _stringBuilder;
    private T _tempItem;
    private List<T> _container = new();
    // containerMap : value : 각 garbageType count
    private Dictionary<GarbageType, int> _containerMap = new();
    private Func<int, Vector3> _getPosition;

    public void Initialize(string key, Func<int, Vector3> getPosition, Transform pivotCenter)
    {
        this._getPosition = getPosition;
        LoadGarbages(key, pivotCenter);
    }

    public void LoadGarbages(string key, Transform pivotCenter)
    {
        if (!PlayerPrefs.HasKey(key)) return;

        var result = PlayerPrefs.GetString(key);
        for (int i = 0; i < result.Length; i++)
        {
            var garbageType = (GarbageType)Convert.ToInt32(result[i].ToString());
            var garbageObject = GenerateGarbage(garbageType);
            garbageObject.transform.SetParent(pivotCenter);
            garbageObject.transform.localRotation = Quaternion.identity;

            Push((T)garbageObject, duration: 0);
        }
    }

    private GarbageObject GenerateGarbage(GarbageType garbageType)
    {
        GarbageDetailType randomeType = GarbageDetailType.None;
        switch (garbageType)
        {
            case GarbageType.Can:
                randomeType = GarbageDetailType.Can1;
                break;
            case GarbageType.Food:
                randomeType = GarbageDetailType.Food1;
                break;
            case GarbageType.Glass:
                randomeType = GarbageDetailType.Glass1;
                break;
            case GarbageType.Paper:
                randomeType = GarbageDetailType.Paper1;
                break;
            case GarbageType.Plastic:
                randomeType = GarbageDetailType.Plastic1;
                break;
        }
        randomeType += Random.Range(0, 3);
        return FactoryManager.Instance.GetGarbageObject(randomeType, Vector3.zero);
    }

    public void SaveGarbages(string key)
    {
        if (_stringBuilder == null)
        {
            _stringBuilder = new StringBuilder();
        }

        _stringBuilder.Clear();
        foreach (var item in _container)
        {
            _stringBuilder.Append((int)item.GarbageType);
        }

        PlayerPrefs.SetString(key, _stringBuilder.ToString());
    }

    private void UpdateGarbagePosition()
    {
        for (int i = 0; i < _container.Count; i++)
        {
            _container[i].transform.localPosition = _getPosition(i);
        }
    }

    public int Count()
    {
        return _container.Count();
    }

    public void Push(T garbageObject, float duration)
    {
        UpdateGarbagePosition();

        garbageObject.transform.DOLocalMove(_getPosition(_container.Count), duration);

        _container.Add(garbageObject);

        if (_containerMap.ContainsKey(garbageObject.GarbageType) == false)
        {
            _containerMap[garbageObject.GarbageType] = 0;
        }
        _containerMap[garbageObject.GarbageType]++;
    }

    public (bool isContained, T garbageObject) Pop(GarbageType garbageType)
    {
        _tempItem = _container.FirstOrDefault(x => x.GarbageType == garbageType);

        if (_tempItem == null || _tempItem.GarbageType == GarbageType.None)
        {
            Debug.LogError("존재하지 않음");
            return (isContained: false, garbageObject: null);
        }

        _container.Remove(_tempItem);
        _containerMap[_tempItem.GarbageType]--;
        //tempItem.transform.SetParent(null);
        UpdateGarbagePosition();

        return (isContained: true, garbageObject: _tempItem);
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return _containerMap.ContainsKey(garbageType)
            && _containerMap[garbageType] > 0;
    }

    public int GetCountOfGarbageType(GarbageType garbageType)
    {
        if (_containerMap.ContainsKey(garbageType) == false)
        {
            _containerMap[garbageType] = 0;
        }
        return _containerMap[garbageType];
    }
}


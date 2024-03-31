using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerGarbageStackSystem
{
    private static readonly string KEY = "PlayerGarbages";
    private Player _player;
    private GarbageStack<GarbageObject> _myGarbages = new();

    [SerializeField] private Transform pivotCenter;
    [SerializeField] private int upgradeValue = 10;
    [SerializeField] private int maxCount = 20;
    [SerializeField] private float garbageGap = 1f;
    [SerializeField] private int orderCount = 7;

    public void Initialize(Player player)
    {
        this._player = player;
        Debug.Assert(pivotCenter != null, "pivotCenter is null");

        _myGarbages.Initialize(KEY, GetPosition, pivotCenter);
        for (int i = 1; i < Enum.GetNames(typeof(GarbageType)).Length; i++)
        {
            UIManager.Instance.UpdateGarbageAmount((GarbageType)i, _myGarbages.GetCountOfGarbageType((GarbageType)i));
        }
    }

    private Vector3 GetPosition(int index)
    {
        return Vector3.zero
            + (index % orderCount * garbageGap * Vector3.up)
            + (index / orderCount * garbageGap * Vector3.back);
    }

    public void OnUpgrade()
    {
        maxCount += upgradeValue;
    }

    private void UpdateCount(GarbageType garbageType)
    {
        _myGarbages.SaveGarbages(KEY);
        UIManager.Instance.UpdateGarbageAmount(garbageType, _myGarbages.GetCountOfGarbageType(garbageType));
    }

    public bool IsAbleToGetGarbage()
    {
        return _myGarbages.Count() < maxCount;
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return _myGarbages.IsAbleToPopGarbage(garbageType);
    }

    public void OnGarbageHeap(GarbageObject garbageObject, float duration)
    {
        garbageObject.transform.SetParent(pivotCenter);
        garbageObject.transform.localRotation = Quaternion.identity;

        _myGarbages.Push(garbageObject, duration);
        UpdateCount(garbageType: garbageObject.GarbageType);
    }

    public (bool isContained, GarbageObject garbageObject) OnWastebasket(GarbageType garbageType)
    {
        var result = _myGarbages.Pop(garbageType);
        if (result.isContained)
        {
            result.garbageObject.transform.parent = null;
            UpdateCount(garbageType);
        }

        return result;
    }
}

class AreTheySame
{
    public static bool comp(int[] a, int[] b)
    {
        // your code
        if (a == null || b == null)
        {
            return false;
        }
        else if (a.Length != b.Length)
        {
            return false;
        }
        var aList = a.ToList();
        var bList = b.ToList();

        for (int i = 0; i < aList.Count; i++)
        {
            if (IsExit(bList, Math.Pow(aList[i], 2)) == false)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsExit(List<int> bList, double squaredValue)
    {
        for (int i = 0; i < bList.Count; i++)
        {
            if (bList[i] == squaredValue)
            {
                bList.RemoveAt(i);
                return true;
            }
        }

        return false;
    }
}
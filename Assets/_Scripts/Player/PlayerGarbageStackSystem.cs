using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGarbageStackSystem
{
    class GarbageCountInfo
    {
        public GarbageType garbageType;
        public int count;
    }

    Player player;
    GarbageStack<GarbageObject> myGarbages = new GarbageStack<GarbageObject>();

    [SerializeField] Transform pivotCenter;
    [SerializeField] int maxCount = 20;
    [SerializeField] float garbageGap = 1f;
    [SerializeField] int orderCount = 7;

    Dictionary<GarbageType, GarbageCountInfo> garbageCountInfoMap = new Dictionary<GarbageType, GarbageCountInfo>();

    public void Initialize(Player player)
    {
        this.player = player;
        Debug.Assert(pivotCenter != null, "pivotCenter is null");

        myGarbages.Initialize(GetPosition);
    }

    Vector3 GetPosition(int index)
    {
        return Vector3.zero
               + (index % orderCount * garbageGap * Vector3.up)
               + (index / orderCount * garbageGap * Vector3.back);
    }

    GarbageCountInfo GetGarbageCountInfo(GarbageType garbageType)
    {
        if (garbageCountInfoMap.ContainsKey(garbageType) == false)
        {
            InitializeGarbageCountMap(garbageType);
        }

        return garbageCountInfoMap[garbageType];

        void InitializeGarbageCountMap(GarbageType garbageType)
        {
            garbageCountInfoMap[garbageType] = new GarbageCountInfo()
            {
                garbageType = garbageType,
                count = 0,
            };
        }
    }

    void UpdateCount(GarbageType garbageType, int changeValue)
    {
        var garbageInfo = GetGarbageCountInfo(garbageType);
        garbageInfo.count += changeValue;
        UIManager.Instance.UpdateGarbageAmount(garbageType, garbageInfo.count);
    }

    void IncreaseCount(GarbageType garbageType)
    {
        UpdateCount(garbageType, +1);
    }

    void DecreaseCount(GarbageType garbageType)
    {
        UpdateCount(garbageType, -1);
    }

    public bool IsAbleToGetGarbage()
    {
        return myGarbages.Count() < maxCount;
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return myGarbages.IsAbleToPopGarbage(garbageType);
    }

    public void OnGarbageHeap(GarbageObject garbageObject, float duration)
    {
        garbageObject.transform.SetParent(pivotCenter);
        garbageObject.transform.localRotation = Quaternion.identity;

        myGarbages.Push(garbageObject, duration);
        IncreaseCount(garbageType: garbageObject.GarbageType);
    }

    public void OnWastebasket(GarbageType garbageType)
    {
        var result = myGarbages.Pop(garbageType);
        if (result.isContained)
        {
            result.garbageObject.transform.parent = null;
        }
        DecreaseCount(garbageType);
    }
}

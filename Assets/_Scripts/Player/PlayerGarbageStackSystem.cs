using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class GarbageStack<T> where T : GarbageObject
{
    readonly List<T> container = new List<T>();
    T tempItem;

    public int Count()
    {
        return container.Count();
    }

    public void Push(T item, Func<int, Vector3> getPosition, float duration)
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].transform.localPosition = getPosition(i);
        }

        item.transform.DOLocalMove(getPosition(container.Count), duration);

        container.Add(item);
    }

    public (bool isContained, T garbageObject) Pop(GarbageType garbageType)
    {
        tempItem = container.FirstOrDefault(x => x.GarbageType == garbageType);

        if (tempItem == null || tempItem.GarbageType == GarbageType.None)
        {
            Debug.LogError("존재하지 않음");
            return (false, null);
        }

        container.Remove(tempItem);
        return (true, tempItem);
    }
}

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

    public void OnGarbageHeap(GarbageObject garbageObject, float duration)
    {
        garbageObject.transform.SetParent(pivotCenter);
        garbageObject.transform.localRotation = Quaternion.identity;

        myGarbages.Push(garbageObject, GetPosition, duration);
        IncreaseCount(garbageType: garbageObject.GarbageType);

        Vector3 GetPosition(int index)
        {
            return Vector3.zero
                   + (index % orderCount * garbageGap * Vector3.up)
                   + (index / orderCount * garbageGap * Vector3.back);
        }
    }

    public void OnWastebasket(GarbageType garbageType)
    {
        myGarbages.Pop(garbageType);
        DecreaseCount(garbageType);
    }
}

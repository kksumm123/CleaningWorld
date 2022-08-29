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

    public (bool isContained, T garbageObject) Pop(GarbageDetailType garbageType)
    {
        tempItem = container.FirstOrDefault(x => x.GarbageDetailType == garbageType);
        if (tempItem.GarbageDetailType == GarbageDetailType.None)
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

    GarbageType GetGarbageTypeFromDetailType(GarbageDetailType garbageDetailType)
    {
        switch (garbageDetailType)
        {
            case GarbageDetailType.Can1:
            case GarbageDetailType.Can2:
            case GarbageDetailType.Can3:
                return GarbageType.Can;
            case GarbageDetailType.Food1:
            case GarbageDetailType.Food2:
            case GarbageDetailType.Food3:
                return GarbageType.Food;
            case GarbageDetailType.Glass1:
            case GarbageDetailType.Glass2:
            case GarbageDetailType.Glass3:
                return GarbageType.Glass;
            case GarbageDetailType.Paper1:
            case GarbageDetailType.Paper2:
            case GarbageDetailType.Paper3:
                return GarbageType.Paper;
            case GarbageDetailType.Plastic1:
            case GarbageDetailType.Plastic2:
            case GarbageDetailType.Plastic3:
                return GarbageType.Plastic;
            default:
                return GarbageType.None;
        }
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

    void UpdateCount(GarbageDetailType garbageDetailType, int changeValue)
    {
        var garbageType = GetGarbageTypeFromDetailType(garbageDetailType);
        var garbageInfo = GetGarbageCountInfo(garbageType);
        garbageInfo.count += changeValue;
        UIManager.Instance.UpdateGarbageAmount(garbageType, garbageInfo.count);
    }

    void IncreaseCount(GarbageDetailType garbageDetailType)
    {
        UpdateCount(garbageDetailType, +1);
    }

    void DecreaseCount(GarbageDetailType garbageDetailType)
    {
        UpdateCount(garbageDetailType, -1);
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
        IncreaseCount(garbageDetailType: garbageObject.GarbageDetailType);

        Vector3 GetPosition(int index)
        {
            return Vector3.zero
                   + (index % orderCount * garbageGap * Vector3.up)
                   + (index / orderCount * garbageGap * Vector3.back);
        }
    }

    public (bool isContained, GarbageObject garbageObject) OnTrashBins(GarbageDetailType garbageType)
    {
        return myGarbages.Pop(garbageType);
    }

    public void OnWastebasket(GarbageType garbageType)
    {

    }
}

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
    Dictionary<GarbageType, int> containerMap = new Dictionary<GarbageType, int>();
    Func<int, Vector3> getPosition;

    public void Initialize(Func<int, Vector3> getPosition)
    {
        this.getPosition = getPosition;
    }

    void UpdateGarbagePosition()
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].transform.localPosition = getPosition(i);
        }
    }

    public int Count()
    {
        return container.Count();
    }

    public void Push(T garbageObject, float duration)
    {
        UpdateGarbagePosition();

        garbageObject.transform.DOLocalMove(getPosition(container.Count), duration);

        container.Add(garbageObject);

        if (containerMap.ContainsKey(garbageObject.GarbageType) == false)
        {
            containerMap[garbageObject.GarbageType] = 0;
        }
        containerMap[garbageObject.GarbageType]++;
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
        containerMap[tempItem.GarbageType]--;
        tempItem.transform.SetParent(null);
        UpdateGarbagePosition();

        return (true, tempItem);
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return containerMap.ContainsKey(garbageType)
               && containerMap[garbageType] > 0;
    }
}


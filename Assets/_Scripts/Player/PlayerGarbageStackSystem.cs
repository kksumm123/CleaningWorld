using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class MyStack<T> where T : GarbageObject
{
    readonly List<T> container = new List<T>();
    T tempItem;

    public int Count()
    {
        return container.Count();
    }

    public void Push(T item)
    {
        container.Add(item);
    }

    public (bool isContained, T garbageObject) Pop(GarbageType garbageType)
    {
        tempItem = container.FirstOrDefault(x => x.GarbageType == garbageType);
        if (tempItem.GarbageType == GarbageType.None)
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
    Player player;
    MyStack<GarbageObject> myGarbages = new MyStack<GarbageObject>();

    [SerializeField] Transform pivotCenter;
    [SerializeField] float garbageGap = 2.5f;

    public void Initialize(Player player)
    {
        this.player = player;
        Debug.Assert(pivotCenter != null, "pivotCenter is null");
    }

    public void OnGarbageHeap(GarbageObject garbageObject)
    {
        garbageObject.transform.SetParent(player.transform);
        garbageObject.transform.localPosition = pivotCenter.position
                                                + (myGarbages.Count() * garbageGap * Vector3.one);
        myGarbages.Push(garbageObject);
    }

    public (bool isContained, GarbageObject garbageObject) OnTrashBins(GarbageType garbageType)
    {
        return myGarbages.Pop(garbageType);
    }
}

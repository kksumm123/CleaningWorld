using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSystem
{
    int LAST_INDEX => objectPool.Count - 1;

    RecycleObject recycleObjectPrefab;
    RecycleObject tempObject;
    List<RecycleObject> objectPool = new List<RecycleObject>();
    Transform parent;

    int defaultPoolSize;

    public ObjectPoolSystem(RecycleObject recycleObjectPrefab, int defaultPoolSize, Transform parent)
    {
        if (defaultPoolSize <= 0)
        {
            defaultPoolSize = 1;
        }

        this.recycleObjectPrefab = recycleObjectPrefab;
        this.defaultPoolSize = defaultPoolSize;
        this.parent = parent;
    }

    void CreateObject()
    {
        for (int i = 0; i < defaultPoolSize; i++)
        {
            tempObject = MonoBehaviour.Instantiate(recycleObjectPrefab, parent);
            tempObject.InitializedByObjectPoolSystem(Restore);
            tempObject.gameObject.SetActive(false);
            objectPool.Add(tempObject);
        }
    }

    public RecycleObject Get()
    {
        if (objectPool.Count <= 0)
        {
            CreateObject();
        }

        tempObject = objectPool[LAST_INDEX];
        objectPool.RemoveAt(LAST_INDEX);
        tempObject.gameObject.SetActive(true);
        return tempObject;
    }

    public void Restore(RecycleObject recycleObject)
    {
        if (objectPool.Contains(recycleObject)) return;

        recycleObject.gameObject.SetActive(false);
        recycleObject.transform.localScale = Vector3.one;
        recycleObject.transform.SetParent(parent);
        objectPool.Add(recycleObject);
    }
}

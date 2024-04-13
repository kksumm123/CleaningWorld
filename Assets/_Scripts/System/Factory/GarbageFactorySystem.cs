using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFactorySystem : FactorySystem<GarbageObject, GarbageDetailType>
{
    protected override string Label => "Garbage";

    protected override void OnLoadAsset(string id, GarbageObject prefab)
    {
        var newId = (GarbageDetailType)Enum.Parse(typeof(GarbageDetailType), id);
        prefabContainer[newId] = prefab;
        InitializeObjectPool(newId);
    }

    public GarbageObject GetGarbageObject(GarbageDetailType garbageType, Vector3 spawnPoint)
    {
        tempObject = GetObject(garbageType);
        tempObject.transform.position = spawnPoint;
        return tempObject;
    }
}

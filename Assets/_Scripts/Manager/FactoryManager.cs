using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    private PrefabFactorySystem _prefabFactorySystem = new();

    public void Initialize()
    {
        _prefabFactorySystem.Initialize(transform);
    }

    public GarbageObject GetGarbageObject(GarbageDetailType garbageType, Vector3 spawnPosition)
    {
        return _prefabFactorySystem.GetGarbageObject(garbageType, spawnPosition);
    }

    public Coin GetCoin(Vector3 spawnPosition)
    {
        return _prefabFactorySystem.GetCoin(spawnPosition);
    }
}

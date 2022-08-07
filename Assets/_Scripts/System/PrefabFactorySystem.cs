using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactorySystem
{
    Transform factoryManager;
    GarbageObject tempGarbageObject;

    ObjectPoolSystem can1ObjectPool;
    ObjectPoolSystem can2ObjectPool;
    ObjectPoolSystem can3ObjectPool;
    ObjectPoolSystem food1ObjectPool;
    ObjectPoolSystem food2ObjectPool;
    ObjectPoolSystem food3ObjectPool;
    ObjectPoolSystem glass1ObjectPool;
    ObjectPoolSystem glass2ObjectPool;
    ObjectPoolSystem glass3ObjectPool;
    ObjectPoolSystem paper1ObjectPool;
    ObjectPoolSystem paper2ObjectPool;
    ObjectPoolSystem paper3ObjectPool;
    ObjectPoolSystem plastic1ObjectPool;
    ObjectPoolSystem plastic2ObjectPool;
    ObjectPoolSystem plastic3ObjectPool;

    public void Initialize(Transform factoryManager)
    {
        this.factoryManager = factoryManager;

        InitializeObjectPool(ref can1ObjectPool, GarbageType.Can1);
        InitializeObjectPool(ref can2ObjectPool, GarbageType.Can2);
        InitializeObjectPool(ref can3ObjectPool, GarbageType.Can3);
        InitializeObjectPool(ref food1ObjectPool, GarbageType.Food1);
        InitializeObjectPool(ref food2ObjectPool, GarbageType.Food2);
        InitializeObjectPool(ref food3ObjectPool, GarbageType.Food3);
        InitializeObjectPool(ref glass1ObjectPool, GarbageType.Glass1);
        InitializeObjectPool(ref glass2ObjectPool, GarbageType.Glass2);
        InitializeObjectPool(ref glass3ObjectPool, GarbageType.Glass3);
        InitializeObjectPool(ref paper1ObjectPool, GarbageType.Paper1);
        InitializeObjectPool(ref paper2ObjectPool, GarbageType.Paper2);
        InitializeObjectPool(ref paper3ObjectPool, GarbageType.Paper3);
        InitializeObjectPool(ref plastic1ObjectPool, GarbageType.Plastic1);
        InitializeObjectPool(ref plastic2ObjectPool, GarbageType.Plastic2);
        InitializeObjectPool(ref plastic3ObjectPool, GarbageType.Plastic3);
    }

    void InitializeObjectPool(ref ObjectPoolSystem objectPool, GarbageType garbageType)
    {
        objectPool = new ObjectPoolSystem(GetGarbageObjectPrefab(garbageType),
                                          defaultPoolSize: 1,
                                          parent: factoryManager);
    }

    GarbageObject GetGarbageObjectPrefab(GarbageType garbageType)
    {
        return GameResourcesManager.Instance.GetGarbageObjectPrefab(garbageType);
    }

    ObjectPoolSystem GetObjectPoolSystem(GarbageType garbageType)
    {
        return garbageType switch
        {
            GarbageType.Can1 => can1ObjectPool,
            GarbageType.Can2 => can2ObjectPool,
            GarbageType.Can3 => can3ObjectPool,
            GarbageType.Food1 => food1ObjectPool,
            GarbageType.Food2 => food2ObjectPool,
            GarbageType.Food3 => food3ObjectPool,
            GarbageType.Glass1 => glass1ObjectPool,
            GarbageType.Glass2 => glass2ObjectPool,
            GarbageType.Glass3 => glass3ObjectPool,
            GarbageType.Paper1 => paper1ObjectPool,
            GarbageType.Paper2 => paper2ObjectPool,
            GarbageType.Paper3 => paper3ObjectPool,
            GarbageType.Plastic1 => plastic1ObjectPool,
            GarbageType.Plastic2 => plastic2ObjectPool,
            GarbageType.Plastic3 => plastic3ObjectPool,
            _ => null,
        };
    }

    public GarbageObject GetGarbageObject(GarbageType garbageType, Vector3 spawnPoint)
    {
        tempGarbageObject = GetObjectPoolSystem(garbageType).Get() as GarbageObject;
        tempGarbageObject.transform.position = spawnPoint;
        return tempGarbageObject;
    }
}

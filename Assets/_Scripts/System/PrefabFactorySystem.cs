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

        InitializeObjectPool(ref can1ObjectPool, GarbageDetailType.Can1);
        InitializeObjectPool(ref can2ObjectPool, GarbageDetailType.Can2);
        InitializeObjectPool(ref can3ObjectPool, GarbageDetailType.Can3);
        InitializeObjectPool(ref food1ObjectPool, GarbageDetailType.Food1);
        InitializeObjectPool(ref food2ObjectPool, GarbageDetailType.Food2);
        InitializeObjectPool(ref food3ObjectPool, GarbageDetailType.Food3);
        InitializeObjectPool(ref glass1ObjectPool, GarbageDetailType.Glass1);
        InitializeObjectPool(ref glass2ObjectPool, GarbageDetailType.Glass2);
        InitializeObjectPool(ref glass3ObjectPool, GarbageDetailType.Glass3);
        InitializeObjectPool(ref paper1ObjectPool, GarbageDetailType.Paper1);
        InitializeObjectPool(ref paper2ObjectPool, GarbageDetailType.Paper2);
        InitializeObjectPool(ref paper3ObjectPool, GarbageDetailType.Paper3);
        InitializeObjectPool(ref plastic1ObjectPool, GarbageDetailType.Plastic1);
        InitializeObjectPool(ref plastic2ObjectPool, GarbageDetailType.Plastic2);
        InitializeObjectPool(ref plastic3ObjectPool, GarbageDetailType.Plastic3);
    }

    void InitializeObjectPool(ref ObjectPoolSystem objectPool, GarbageDetailType garbageType)
    {
        objectPool = new ObjectPoolSystem(GetGarbageObjectPrefab(garbageType),
                                          defaultPoolSize: 1,
                                          parent: factoryManager);
    }

    GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        return GameResourcesManager.Instance.GetGarbageObjectPrefab(garbageType);
    }

    ObjectPoolSystem GetObjectPoolSystem(GarbageDetailType garbageType)
    {
        return garbageType switch
        {
            GarbageDetailType.Can1 => can1ObjectPool,
            GarbageDetailType.Can2 => can2ObjectPool,
            GarbageDetailType.Can3 => can3ObjectPool,
            GarbageDetailType.Food1 => food1ObjectPool,
            GarbageDetailType.Food2 => food2ObjectPool,
            GarbageDetailType.Food3 => food3ObjectPool,
            GarbageDetailType.Glass1 => glass1ObjectPool,
            GarbageDetailType.Glass2 => glass2ObjectPool,
            GarbageDetailType.Glass3 => glass3ObjectPool,
            GarbageDetailType.Paper1 => paper1ObjectPool,
            GarbageDetailType.Paper2 => paper2ObjectPool,
            GarbageDetailType.Paper3 => paper3ObjectPool,
            GarbageDetailType.Plastic1 => plastic1ObjectPool,
            GarbageDetailType.Plastic2 => plastic2ObjectPool,
            GarbageDetailType.Plastic3 => plastic3ObjectPool,
            _ => null,
        };
    }

    public GarbageObject GetGarbageObject(GarbageDetailType garbageType, Vector3 spawnPoint)
    {
        tempGarbageObject = GetObjectPoolSystem(garbageType).Get() as GarbageObject;
        tempGarbageObject.transform.position = spawnPoint;
        return tempGarbageObject;
    }
}

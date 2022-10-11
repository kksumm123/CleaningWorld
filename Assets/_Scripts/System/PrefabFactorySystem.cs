using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactorySystem
{
    Transform factoryManager;
    GarbageObject tempGarbageObject;
    Coin tempCoin;

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
    ObjectPoolSystem coinObjectPool;

    public void Initialize(Transform factoryManager)
    {
        this.factoryManager = factoryManager;

        InitializeGarbageObjectPool(ref can1ObjectPool, GarbageDetailType.Can1);
        InitializeGarbageObjectPool(ref can2ObjectPool, GarbageDetailType.Can2);
        InitializeGarbageObjectPool(ref can3ObjectPool, GarbageDetailType.Can3);
        InitializeGarbageObjectPool(ref food1ObjectPool, GarbageDetailType.Food1);
        InitializeGarbageObjectPool(ref food2ObjectPool, GarbageDetailType.Food2);
        InitializeGarbageObjectPool(ref food3ObjectPool, GarbageDetailType.Food3);
        InitializeGarbageObjectPool(ref glass1ObjectPool, GarbageDetailType.Glass1);
        InitializeGarbageObjectPool(ref glass2ObjectPool, GarbageDetailType.Glass2);
        InitializeGarbageObjectPool(ref glass3ObjectPool, GarbageDetailType.Glass3);
        InitializeGarbageObjectPool(ref paper1ObjectPool, GarbageDetailType.Paper1);
        InitializeGarbageObjectPool(ref paper2ObjectPool, GarbageDetailType.Paper2);
        InitializeGarbageObjectPool(ref paper3ObjectPool, GarbageDetailType.Paper3);
        InitializeGarbageObjectPool(ref plastic1ObjectPool, GarbageDetailType.Plastic1);
        InitializeGarbageObjectPool(ref plastic2ObjectPool, GarbageDetailType.Plastic2);
        InitializeGarbageObjectPool(ref plastic3ObjectPool, GarbageDetailType.Plastic3);
        coinObjectPool = new ObjectPoolSystem(recycleObjectPrefab: GameResourcesManager.Instance.GetCoinPrefab(),
                                              defaultPoolSize: 1,
                                              parent: factoryManager);
    }

    void InitializeGarbageObjectPool(ref ObjectPoolSystem objectPool, GarbageDetailType garbageType)
    {
        objectPool = new ObjectPoolSystem(GetGarbageObjectPrefab(garbageType),
                                          defaultPoolSize: 1,
                                          parent: factoryManager);
    }

    GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        return GameResourcesManager.Instance.GetGarbageObjectPrefab(garbageType);
    }

    ObjectPoolSystem GetGarbageObjectPoolSystem(GarbageDetailType garbageType)
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
            _ => ReturnNull(garbageType),
        };
    }

    private ObjectPoolSystem ReturnNull(GarbageDetailType garbageType)
    {
        Debug.LogError($"존재하지 않는 타입 {garbageType}");
        return null;
    }

    public GarbageObject GetGarbageObject(GarbageDetailType garbageType, Vector3 spawnPoint)
    {
        tempGarbageObject = GetGarbageObjectPoolSystem(garbageType).Get() as GarbageObject;
        tempGarbageObject.transform.position = spawnPoint;
        return tempGarbageObject;
    }

    public Coin GetCoin(Vector3 spawnPosition)
    {
        tempCoin = coinObjectPool.Get() as Coin;
        tempCoin.transform.position = spawnPosition;
        return tempCoin;
    }
}

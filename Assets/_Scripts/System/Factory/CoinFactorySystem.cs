using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFactorySystem : FactorySystem<Coin, int>
{
    protected override string Label => "Coin";

    protected override void OnLoadAsset(string id, Coin prefab)
    {
        var newId = Convert.ToInt32(id);
        prefabContainer[newId] = prefab;
        InitializeObjectPool(newId);
    }

    public Coin GetCoin(Vector3 spawnPosition)
    {
        tempObject = GetObject(100);
        tempObject.transform.position = spawnPosition;
        return tempObject;
    }
}

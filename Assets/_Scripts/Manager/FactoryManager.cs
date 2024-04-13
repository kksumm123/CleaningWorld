using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    private GarbageFactorySystem _garbageFactorySystem = new();
    private CoinFactorySystem _coinFactorySystem = new();

    public async Task Initialize()
    {
        await _garbageFactorySystem.Initialize(transform);
        await _coinFactorySystem.Initialize(transform);
    }

    public GarbageObject GetGarbageObject(GarbageDetailType garbageType, Vector3 spawnPosition)
    {
        return _garbageFactorySystem.GetGarbageObject(garbageType, spawnPosition);
    }

    public Coin GetCoin(Vector3 spawnPosition)
    {
        return _coinFactorySystem.GetCoin(spawnPosition);
    }
}

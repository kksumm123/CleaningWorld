using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesManager : Singleton<GameResourcesManager>
{
    private GameResourcesSystem _gameResourcesSystem = new();

    public void Initialize()
    {
        _gameResourcesSystem.Initialize();
    }

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        return _gameResourcesSystem.GetGarbageObjectPrefab(garbageType);
    }

    public Sprite GetIcon(IconType iconType)
    {
        return _gameResourcesSystem.GetIcon(iconType);
    }

    public Coin GetCoinPrefab()
    {
        return _gameResourcesSystem.GetCoinPrefab();
    }
}

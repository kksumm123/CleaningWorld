using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesManager : Singleton<GameResourcesManager>
{
    GameResourcesSystem gameResourcesSystem = new GameResourcesSystem();

    public void Initialize()
    {
        gameResourcesSystem.Initialize();
    }

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        return gameResourcesSystem.GetGarbageObjectPrefab(garbageType);
    }

    public Sprite GetIcon(IconType iconType)
    {
        return gameResourcesSystem.GetIcon(iconType);
    }

    public Coin GetCoinPrefab()
    {
        return gameResourcesSystem.GetCoinPrefab();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesSystem
{
    PrefabResourcesInfoSO prefabResourcesInfoSO;
    ImageResourcesInfoSO imageResourcesInfoSO;
    //PrefabResourcesInfoSO particleResourcesInfoSO;

    public void Initialize()
    {
        prefabResourcesInfoSO = Resources.Load<PrefabResourcesInfoSO>(nameof(PrefabResourcesInfoSO));
        imageResourcesInfoSO = Resources.Load<ImageResourcesInfoSO>(nameof(ImageResourcesInfoSO));
    }

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        return prefabResourcesInfoSO.GetGarbageObjectPrefab(garbageType);
    }

    public Coin GetCoinPrefab()
    {
        return prefabResourcesInfoSO.GetCoinPrefab();
    }

    public Sprite GetIcon(IconType iconType)
    {
        return imageResourcesInfoSO.GetIcon(iconType);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesSystem
{
    private PrefabResourcesInfoSO _prefabResourcesInfoSO;
    private ImageResourcesInfoSO _imageResourcesInfoSO;
    //private PrefabResourcesInfoSO particleResourcesInfoSO;

    public void Initialize()
    {
        _prefabResourcesInfoSO = Resources.Load<PrefabResourcesInfoSO>(nameof(PrefabResourcesInfoSO));
        _imageResourcesInfoSO = Resources.Load<ImageResourcesInfoSO>(nameof(ImageResourcesInfoSO));
    }

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        return _prefabResourcesInfoSO.GetGarbageObjectPrefab(garbageType);
    }

    public Coin GetCoinPrefab()
    {
        return _prefabResourcesInfoSO.GetCoinPrefab();
    }

    public Sprite GetIcon(IconType iconType)
    {
        return _imageResourcesInfoSO.GetIcon(iconType);
    }
}

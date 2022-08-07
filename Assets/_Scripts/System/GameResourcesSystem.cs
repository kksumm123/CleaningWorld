using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesSystem
{
    PrefabResourcesInfoSO prefabResourcesInfoSO;
    PrefabResourcesInfoSO particleResourcesInfoSO;
    PrefabResourcesInfoSO imageResourcesInfoSO;

    public void Initialize()
    {
        prefabResourcesInfoSO = Resources.Load<PrefabResourcesInfoSO>(nameof(PrefabResourcesInfoSO));
    }

    public GarbageObject GetGarbageObjectPrefab(GarbageType garbageType)
    {
        return prefabResourcesInfoSO.GetGarbageObjectPrefab(garbageType);
    }
}

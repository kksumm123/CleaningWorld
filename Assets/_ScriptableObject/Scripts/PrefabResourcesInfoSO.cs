using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PrefabResourcesInfoSO), menuName = "CleaningWorld/PrefabResourcesInfoSO")]
public class PrefabResourcesInfoSO : ScriptableObject
{
    [SerializeField] GarbageObject can1Prefab;
    [SerializeField] GarbageObject can2Prefab;
    [SerializeField] GarbageObject can3Prefab;
    [SerializeField] GarbageObject food1Prefab;
    [SerializeField] GarbageObject food2Prefab;
    [SerializeField] GarbageObject food3Prefab;
    [SerializeField] GarbageObject Glass1Prefab;
    [SerializeField] GarbageObject Glass2Prefab;
    [SerializeField] GarbageObject Glass3Prefab;
    [SerializeField] GarbageObject paper1Prefab;
    [SerializeField] GarbageObject paper2Prefab;
    [SerializeField] GarbageObject paper3Prefab;
    [SerializeField] GarbageObject Platic1Prefab;
    [SerializeField] GarbageObject Platic2Prefab;
    [SerializeField] GarbageObject Platic3Prefab;

    public GarbageObject GetGarbageObjectPrefab(GarbageType garbageType)
    {
        switch (garbageType)
        {
            case GarbageType.Can1:
                return can1Prefab;
            case GarbageType.Can2:
                return can2Prefab;
            case GarbageType.Can3:
                return can3Prefab;
            case GarbageType.Food1:
                return food1Prefab;
            case GarbageType.Food2:
                return food2Prefab;
            case GarbageType.Food3:
                return food3Prefab;
            case GarbageType.Glass1:
                return Glass1Prefab;
            case GarbageType.Glass2:
                return Glass2Prefab;
            case GarbageType.Glass3:
                return Glass3Prefab;
            case GarbageType.Paper1:
                return paper1Prefab;
            case GarbageType.Paper2:
                return paper2Prefab;
            case GarbageType.Paper3:
                return paper3Prefab;
            case GarbageType.Plastic1:
                return Platic1Prefab;
            case GarbageType.Plastic2:
                return Platic2Prefab;
            case GarbageType.Plastic3:
                return Platic3Prefab;
            default:
                Debug.Log($"PrefabResourcesInfoSO : 이게 호출되면 안됨, garbageType = {garbageType}");
                return null;
        }
    }
}

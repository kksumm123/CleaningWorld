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
    [SerializeField] Coin coinPrefab;

    public GarbageObject GetGarbageObjectPrefab(GarbageDetailType garbageType)
    {
        switch (garbageType)
        {
            case GarbageDetailType.Can1:
                return can1Prefab;
            case GarbageDetailType.Can2:
                return can2Prefab;
            case GarbageDetailType.Can3:
                return can3Prefab;
            case GarbageDetailType.Food1:
                return food1Prefab;
            case GarbageDetailType.Food2:
                return food2Prefab;
            case GarbageDetailType.Food3:
                return food3Prefab;
            case GarbageDetailType.Glass1:
                return Glass1Prefab;
            case GarbageDetailType.Glass2:
                return Glass2Prefab;
            case GarbageDetailType.Glass3:
                return Glass3Prefab;
            case GarbageDetailType.Paper1:
                return paper1Prefab;
            case GarbageDetailType.Paper2:
                return paper2Prefab;
            case GarbageDetailType.Paper3:
                return paper3Prefab;
            case GarbageDetailType.Plastic1:
                return Platic1Prefab;
            case GarbageDetailType.Plastic2:
                return Platic2Prefab;
            case GarbageDetailType.Plastic3:
                return Platic3Prefab;
            default:
                Debug.Log($"PrefabResourcesInfoSO : 이게 호출되면 안됨, garbageType = {garbageType}");
                return null;
        }
    }

    public Coin GetCoinPrefab() => coinPrefab;
}

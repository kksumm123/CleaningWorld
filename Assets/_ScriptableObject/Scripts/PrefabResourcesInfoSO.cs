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
}


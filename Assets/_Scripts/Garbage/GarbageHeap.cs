using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeap : MonoBehaviour
{
    int garbageTypesMaxNumber;
    [SerializeField] GarbageHeapPlayerDetector garbageHeapPlayerDetector;
    [SerializeField] float delay = 0.5f;

    void Start()
    {
        Debug.Assert(garbageHeapPlayerDetector != null,
                     "garbageHeapPlayerDetector is null");

        var garbageTypes = Enum.GetNames(typeof(GarbageType));
        garbageTypesMaxNumber = garbageTypes.Length;

        garbageHeapPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
    }

    void OnPlayerEnter()
    {
        StopGenerateGarbageCo();
        GetGeneratebageCoHandle = StartCoroutine(GenerateGarbageCo());
    }

    void StopGenerateGarbageCo()
    {
        if (GetGeneratebageCoHandle != null)
        {
            StopCoroutine(GetGeneratebageCoHandle);
        }
    }

    void OnPlayerExit()
    {
        StopGenerateGarbageCo();
    }

    Coroutine GetGeneratebageCoHandle;
    IEnumerator GenerateGarbageCo()
    {
        var isTrue = true;
        while (isTrue)
        {
            OnGarbageHeap();
            yield return new WaitForSeconds(delay);
        }
    }

    void OnGarbageHeap()
    {
        if (Player.Instance.IsAbleToGetGarbage() == false)
        {
            return;
        }

        Player.Instance.OnGarbageHeap(GenerateGarbage(), delay);
    }

    GarbageObject GenerateGarbage()
    {
        GarbageType randomeType = (GarbageType)Random.Range(1, garbageTypesMaxNumber);

        var randomGarbage = FactoryManager.Instance
                                          .GetGarbageObject(randomeType,
                                                            transform.position);
        return randomGarbage;
    }
}

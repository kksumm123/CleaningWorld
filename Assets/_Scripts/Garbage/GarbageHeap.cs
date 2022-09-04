using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeap : MonoBehaviour
{
    int garbageTypesMaxNumber;
    [SerializeField] GarbageHeapPlayerDetector garbageHeapPlayerDetector;
    [SerializeField] GarbageAmountBox garbageAmountBox;
    [SerializeField] float delay = 0.5f;

    [SerializeField] Transform inner;
    [SerializeField] int garbageCount = 100;
    int originGarbageCount;
    Vector3 originScale;

    void Start()
    {
        WoonyMethods.Assert(this, (garbageHeapPlayerDetector, nameof(garbageHeapPlayerDetector)),
                                  (garbageAmountBox, nameof(garbageAmountBox)),
                                  (inner, nameof(inner)));

        var garbageTypes = Enum.GetNames(typeof(GarbageDetailType));
        garbageTypesMaxNumber = garbageTypes.Length;

        garbageHeapPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
        garbageAmountBox.Initialize();
        garbageAmountBox.UpdateAmount(garbageCount);

        originGarbageCount = garbageCount;
        originScale = inner.localScale;
    }

    void SubGarbageCount(int value)
    {
        value = Math.Abs(value);
        garbageCount -= value;
        garbageAmountBox.UpdateAmount(garbageCount);
    }

    void OnPlayerEnter()
    {
        StopGenerateGarbageCo();
        getGeneratebageCoHandle = StartCoroutine(GenerateGarbageCo());
    }

    void StopGenerateGarbageCo()
    {
        if (getGeneratebageCoHandle != null)
        {
            StopCoroutine(getGeneratebageCoHandle);
        }
    }

    void OnPlayerExit()
    {
        StopGenerateGarbageCo();
    }

    Coroutine getGeneratebageCoHandle;
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
        if (Player.Instance.IsAbleToGetGarbage() == false
            || IsAbleToGenerateGarbage() == false)
        {
            return;
        }

        Player.Instance.OnGarbageHeap(GenerateGarbage(), delay);
    }

    private bool IsAbleToGenerateGarbage()
    {
        return garbageCount > 0;
    }

    GarbageObject GenerateGarbage()
    {
        GarbageDetailType randomeType = (GarbageDetailType)Random.Range(1, garbageTypesMaxNumber);

        var randomGarbage = FactoryManager.Instance
                                          .GetGarbageObject(randomeType,
                                                            transform.position);
        OnGenerateGarbage();

        return randomGarbage;

        void OnGenerateGarbage()
        {
            SubGarbageCount(1);
            inner.localScale = garbageCount / (float)originGarbageCount * originScale;
        }
    }
}

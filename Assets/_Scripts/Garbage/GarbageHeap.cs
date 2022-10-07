using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeap : MonoBehaviour
{
    static List<GarbageHeap> garbageHeaps = new List<GarbageHeap>();

    static readonly string BASE_KEY = "GarbageHeap";
    string REAL_KEY => BASE_KEY + uid;

    [SerializeField] int uid;

    int garbageTypesMaxNumber;
    [SerializeField] GarbageHeapPlayerDetector garbageHeapPlayerDetector;
    [SerializeField] GarbageAmountBox garbageAmountBox;
    [SerializeField] float delay = 0.5f;

    [SerializeField] Transform inner;
    [SerializeField] int initializeGarbageCount = 100;
    int garbageCount;
    int originGarbageCount;
    Vector3 originScale;

    void Start()
    {
        garbageHeaps.ForEach(x =>
        {
            if (x.uid == uid)
            {
                Debug.LogError($"동일한 uid를 가진 개체가 존재합니다", x.transform);
                Debug.LogError($"동일한 uid를 가진 개체가 존재합니다", transform);
            }
        });

        garbageHeaps.Add(this);

        WoonyMethods.Assert(this, (garbageHeapPlayerDetector, nameof(garbageHeapPlayerDetector)),
                                  (garbageAmountBox, nameof(garbageAmountBox)),
                                  (inner, nameof(inner)));
        LoadData();

        var garbageTypes = Enum.GetNames(typeof(GarbageDetailType));
        garbageTypesMaxNumber = garbageTypes.Length;

        garbageHeapPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
        garbageAmountBox.Initialize();
        garbageAmountBox.UpdateAmount(garbageCount);

        originGarbageCount = initializeGarbageCount;
        originScale = inner.localScale;
    }

    private void OnDestroy()
    {
        garbageHeaps.Remove(this);
    }

    void LoadData()
    {
        garbageCount = PlayerPrefs.GetInt(REAL_KEY, initializeGarbageCount);
    }

    void SaveData()
    {
        PlayerPrefs.SetInt(REAL_KEY, garbageCount);
    }

    void OnPlayerEnter()
    {
        StopGenerateGarbageCo();
        getGeneratebageCoHandle = StartCoroutine(GenerateGarbageCo());
    }

    void OnPlayerExit()
    {
        StopGenerateGarbageCo();
    }

    void StopGenerateGarbageCo()
    {
        if (getGeneratebageCoHandle != null)
        {
            StopCoroutine(getGeneratebageCoHandle);
        }
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

    void SubGarbageCount(int value)
    {
        value = Math.Abs(value);
        garbageCount -= value;
        garbageAmountBox.UpdateAmount(garbageCount);
        SaveData();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeap : MonoBehaviour
{
    private static List<GarbageHeap> garbageHeaps = new List<GarbageHeap>();

    private static readonly string BASE_KEY = "GarbageHeap";
    private string REAL_KEY => BASE_KEY + uid;

    [SerializeField] private int uid;

    private int _garbageTypesMaxNumber;
    [SerializeField] private GarbageHeapPlayerDetector garbageHeapPlayerDetector;
    [SerializeField] private GarbageAmountBox garbageAmountBox;
    [SerializeField] private float delay = 0.5f;

    [SerializeField] private Transform inner;
    [SerializeField] private int initializeGarbageCount = 100;
    private int _garbageCount;
    private int _originGarbageCount;
    private Vector3 _originScale;

    private Coroutine _getGeneratebageCoHandle;

    private void Start()
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

        WoonyMethods.Assert(this,
            (garbageHeapPlayerDetector, nameof(garbageHeapPlayerDetector)),
            (garbageAmountBox, nameof(garbageAmountBox)),
            (inner, nameof(inner)));
        LoadData();

        var garbageTypes = Enum.GetNames(typeof(GarbageDetailType));
        _garbageTypesMaxNumber = garbageTypes.Length;

        garbageHeapPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExit);
        garbageAmountBox.Initialize();
        garbageAmountBox.UpdateAmount(_garbageCount);

        _originGarbageCount = initializeGarbageCount;
        _originScale = inner.localScale;
    }

    private void OnDestroy()
    {
        garbageHeaps.Remove(this);
    }

    private void LoadData()
    {
        _garbageCount = PlayerPrefs.GetInt(REAL_KEY, initializeGarbageCount);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(REAL_KEY, _garbageCount);
    }

    private void OnPlayerEnter()
    {
        StopGenerateGarbageCo();
        _getGeneratebageCoHandle = StartCoroutine(GenerateGarbageCo());
    }

    private void OnPlayerExit()
    {
        StopGenerateGarbageCo();
    }

    private void StopGenerateGarbageCo()
    {
        if (_getGeneratebageCoHandle != null)
        {
            StopCoroutine(_getGeneratebageCoHandle);
        }
    }

    private IEnumerator GenerateGarbageCo()
    {
        var isTrue = true;
        while (isTrue)
        {
            OnGarbageHeap();
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnGarbageHeap()
    {
        if (!IsValid()) return;

        Player.Instance.OnGarbageHeap(GenerateGarbage(), delay);

        bool IsValid() => Player.Instance.IsAbleToGetGarbage() && IsAbleToGenerateGarbage();
    }

    private bool IsAbleToGenerateGarbage()
    {
        return _garbageCount > 0;
    }

    private GarbageObject GenerateGarbage()
    {
        GarbageDetailType randomeType = (GarbageDetailType)Random.Range(1, _garbageTypesMaxNumber);

        var randomGarbage = FactoryManager.Instance.GetGarbageObject(randomeType, transform.position);
        OnGenerateGarbage();

        return randomGarbage;

        void OnGenerateGarbage()
        {
            SubGarbageCount(1);
            inner.localScale = _garbageCount / (float)_originGarbageCount * _originScale;
        }
    }

    private void SubGarbageCount(int value)
    {
        value = Math.Abs(value);
        _garbageCount -= value;
        garbageAmountBox.UpdateAmount(_garbageCount);
        SaveData();
    }
}

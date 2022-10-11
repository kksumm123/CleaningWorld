using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

class GarbageStack<T> where T : GarbageObject
{
    readonly List<T> container = new List<T>();
    StringBuilder stringBuilder;
    T tempItem;
    // containerMap은 각 쓰레기별 카운트를 저장하기 위해 사용된 딕셔너리
    Dictionary<GarbageType, int> containerMap = new Dictionary<GarbageType, int>();
    Func<int, Vector3> getPosition;

    public void Initialize(string key, Func<int, Vector3> getPosition, Transform pivotCenter)
    {
        this.getPosition = getPosition;
        LoadGarbages(key, pivotCenter);
    }

    public void LoadGarbages(string key, Transform pivotCenter)
    {
        if (PlayerPrefs.HasKey(key))
        {
            var result = PlayerPrefs.GetString(key);

            for (int i = 0; i < result.Length; i++)
            {
                var garbageType = (GarbageType)Convert.ToInt32(result[i].ToString());
                var garbageObject = GenerateGarbage(garbageType);
                garbageObject.transform.SetParent(pivotCenter);
                garbageObject.transform.localRotation = Quaternion.identity;

                Push((T)garbageObject,
                     duration: 0);
            }
        }
    }

    GarbageObject GenerateGarbage(GarbageType garbageType)
    {
        GarbageDetailType randomeType = GarbageDetailType.None;
        switch (garbageType)
        {
            case GarbageType.Can:
                randomeType = GarbageDetailType.Can1;
                break;
            case GarbageType.Food:
                randomeType = GarbageDetailType.Food1;
                break;
            case GarbageType.Glass:
                randomeType = GarbageDetailType.Glass1;
                break;
            case GarbageType.Paper:
                randomeType = GarbageDetailType.Paper1;
                break;
            case GarbageType.Plastic:
                randomeType = GarbageDetailType.Plastic1;
                break;
        }
        randomeType += Random.Range(0, 3);
        return FactoryManager.Instance
                             .GetGarbageObject(randomeType,
                                               Vector3.zero);
    }

    public void SaveGarbages(string key)
    {
        if (stringBuilder == null)
        {
            stringBuilder = new StringBuilder();
        }

        stringBuilder.Clear();
        foreach (var item in container)
        {
            stringBuilder.Append((int)item.GarbageType);
        }

        PlayerPrefs.SetString(key, stringBuilder.ToString());
    }

    void UpdateGarbagePosition()
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].transform.localPosition = getPosition(i);
        }
    }

    public int Count()
    {
        return container.Count();
    }

    public void Push(T garbageObject, float duration)
    {
        UpdateGarbagePosition();

        garbageObject.transform.DOLocalMove(getPosition(container.Count), duration);

        container.Add(garbageObject);

        if (containerMap.ContainsKey(garbageObject.GarbageType) == false)
        {
            containerMap[garbageObject.GarbageType] = 0;
        }
        containerMap[garbageObject.GarbageType]++;
    }

    public (bool isContained, T garbageObject) Pop(GarbageType garbageType)
    {
        tempItem = container.FirstOrDefault(x => x.GarbageType == garbageType);

        if (tempItem == null || tempItem.GarbageType == GarbageType.None)
        {
            Debug.LogError("존재하지 않음");
            return (false, null);
        }

        container.Remove(tempItem);
        containerMap[tempItem.GarbageType]--;
        //tempItem.transform.SetParent(null);
        UpdateGarbagePosition();

        return (true, tempItem);
    }

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return containerMap.ContainsKey(garbageType)
               && containerMap[garbageType] > 0;
    }

    public int GetCountOfGarbageType(GarbageType garbageType)
    {
        if (containerMap.ContainsKey(garbageType) == false)
        {
            containerMap[garbageType] = 0;
        }
        return containerMap[garbageType];
    }
}


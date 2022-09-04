using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    [SerializeField] GarbageType garbageType;
    [SerializeField] WastebasketPlayerDetector wastebasketPlayerDetector;
    [SerializeField] float delay = 0.15f;

    void Start()
    {
        WoonyMethods.Assert(this, (wastebasketPlayerDetector, nameof(wastebasketPlayerDetector)));
        Debug.Assert(garbageType != GarbageType.None, "타입 설정이 필요함", transform);

        wastebasketPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExist);
    }

    void OnPlayerEnter()
    {
        StopCo();
        onPlayerEnterCoHandle = StartCoroutine(OnPlayerEnterCo());
    }

    void OnPlayerExist()
    {
        StopCo();
    }

    void StopCo()
    {
        if (onPlayerEnterCoHandle != null)
        {
            StopCoroutine(onPlayerEnterCoHandle);
        }
    }

    Coroutine onPlayerEnterCoHandle;
    IEnumerator OnPlayerEnterCo()
    {
        var isTrue = true;
        while (isTrue && Player.Instance.IsAbleToPopGarbage(garbageType))
        {
            var result = Player.Instance.OnWastebasket(garbageType);
            
            yield return result.garbageObject.transform.DOMove(transform.position, delay)
                                                       .WaitForCompletion();
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    [SerializeField] GarbageType garbageType;
    [SerializeField] WastebasketPlayerDetector wastebasketPlayerDetector;
    [SerializeField] WastebasketLidController lidController;
    [SerializeField] Transform garbageArrivedPoint;
    [SerializeField] float addedYValue = 2;
    [SerializeField] float delay = 0.15f;

    void Start()
    {
        WoonyMethods.Assert(this, (wastebasketPlayerDetector, nameof(wastebasketPlayerDetector)),
                                  (lidController, nameof(lidController)),
                                  (garbageArrivedPoint, nameof(garbageArrivedPoint)));
        Debug.Assert(garbageType != GarbageType.None, "타입 설정이 필요함", transform);

        wastebasketPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExist);
        lidController.Initialize();
    }

    void OnPlayerEnter()
    {
        StopCo();
        onPlayerEnterCoHandle = StartCoroutine(OnPlayerEnterCo());
    }

    void OnPlayerExist()
    {
        StopCo();
        lidController.Close();
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
        yield return lidController.Open()
                                  .WaitForCompletion();

        var isTrue = true;
        while (isTrue && Player.Instance.IsAbleToPopGarbage(garbageType))
        {
            (bool isContained, GarbageObject garbageObject) = Player.Instance.OnWastebasket(garbageType);

            // 쓰레기 날리기
            yield return garbageObject.OnWastebasket(garbageArrivedPoint.position,
                                                     addedYValue,
                                                     delay)
                                      .WaitForCompletion();

            // 코인 지급
            var newCoin = FactoryManager.Instance.GetCoin(garbageArrivedPoint.position);
            newCoin.FlyToPlayer();
        }
    }
}

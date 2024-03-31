using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wastebasket : MonoBehaviour
{
    [SerializeField] private GarbageType garbageType;
    [SerializeField] private WastebasketPlayerDetector wastebasketPlayerDetector;
    [SerializeField] private WastebasketLidController lidController;
    [SerializeField] private Transform garbageArrivedPoint;
    [SerializeField] private float addedYValue = 2;
    [SerializeField] private float delay = 0.15f;

    private bool _isPlayerAttached = false;

    private Coroutine onPlayerEnterCoHandle;

    private void Start()
    {
        WoonyMethods.Assert(this,
            (wastebasketPlayerDetector, nameof(wastebasketPlayerDetector)),
            (lidController, nameof(lidController)),
            (garbageArrivedPoint, nameof(garbageArrivedPoint)));
        Debug.Assert(garbageType != GarbageType.None, "타입 설정이 필요함", transform);

        wastebasketPlayerDetector.Initialize(OnPlayerEnter, OnPlayerExist);
        lidController.Initialize();
    }

    private void OnPlayerEnter()
    {
        _isPlayerAttached = true;
        StopCo();
        onPlayerEnterCoHandle = StartCoroutine(OnPlayerEnterCo());
    }

    private void OnPlayerExist()
    {
        _isPlayerAttached = false;
        lidController.Close();
    }

    private void StopCo()
    {
        if (onPlayerEnterCoHandle != null)
        {
            StopCoroutine(onPlayerEnterCoHandle);
        }
    }

    private IEnumerator OnPlayerEnterCo()
    {
        yield return lidController.Open().WaitForCompletion();

        var isTrue = true;
        while (isTrue && Player.Instance.IsAbleToPopGarbage(garbageType) && _isPlayerAttached)
        {
            (bool isContained, GarbageObject garbageObject) = Player.Instance.OnWastebasket(garbageType);

            // 쓰레기 날리기
            var onWastebasketHandle = garbageObject.OnWastebasket(garbageArrivedPoint.position, addedYValue, delay);
            yield return onWastebasketHandle.WaitForCompletion();

            // 코인 지급
            var newCoin = FactoryManager.Instance.GetCoin(garbageArrivedPoint.position);
            newCoin.FlyToPlayer();
        }
    }
}

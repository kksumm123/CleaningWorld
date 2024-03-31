using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageObject : RecycleObject
{
    public GarbageDetailType GarbageDetailType => garbageDetailType;

    [field: SerializeField] public GarbageType GarbageType { get; private set; }
    [SerializeField] private GarbageDetailType garbageDetailType;
    private Sequence _sequence;

    public Tween OnWastebasket(Vector3 garbageArrivedPoint, float addedYValue, float delay)
    {
        if (_sequence != null && _sequence.IsActive() && _sequence.IsPlaying())
        {
            _sequence.Complete();
        }

        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOMove(garbageArrivedPoint, delay));
        _sequence.Join(transform.DOJump(garbageArrivedPoint, addedYValue, 1, delay));
        _sequence.Join(
            transform.DOScale(0, delay * 0.1f)
                .SetDelay(delay * 0.9f)
                .OnComplete(() => Restore()));
        return _sequence;
    }
}

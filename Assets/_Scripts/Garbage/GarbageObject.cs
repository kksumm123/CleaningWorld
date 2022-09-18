using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageObject : RecycleObject
{
    public GarbageDetailType GarbageDetailType => garbageDetailType;

    [field: SerializeField] public GarbageType GarbageType { get; private set; }
    [SerializeField] GarbageDetailType garbageDetailType;
    Sequence sequence;

    public Tween OnWastebasket(Vector3 garbageArrivedPoint, float addedYValue, float delay)
    {
        if (sequence != null && sequence.IsPlaying())
        {
            sequence.Complete();
        }

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(garbageArrivedPoint, delay));
        sequence.Join(transform.DOJump(garbageArrivedPoint, addedYValue, 1, delay));
        sequence.Join(transform.DOScale(0, delay * 0.1f)
                               .SetDelay(delay * 0.9f)
                               .OnComplete(() => Restore()));
        return sequence;
    }
}

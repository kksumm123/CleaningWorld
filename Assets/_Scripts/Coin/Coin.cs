using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : RecycleObject
{
    int value = 1;
    [SerializeField] float addedYValue = 4;
    [SerializeField] float moveYDuration = 0.5f;
    float fadeOutScaleDuration = 0.05f;

    public void FlyToPlayer()
    {
        Player.Instance.AddCoin(value);

        var originalPosition = transform.position;
        var sequence = DOTween.Sequence()
                              .OnComplete(() => Restore());
        sequence.Append(transform.DOMoveY(originalPosition.y + addedYValue,
                                          moveYDuration)
                                 .SetLoops(2, LoopType.Yoyo));

        Vector3 endValue = transform.localRotation.eulerAngles + new Vector3(0, 180, 0);
        sequence.Join(transform.DOLocalRotate(endValue,
                                              moveYDuration * 2));

        sequence.Append(transform.DOScale(0, fadeOutScaleDuration));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageUI : Singleton<GarbageUI>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GarbageUIBox baseUIBox;

    private void Awake()
    {
        WoonyMethods.Assert(this, (canvasGroup, nameof(canvasGroup)));
        Debug.Assert(canvasGroup != null, "canvasGroup is null", transform);

        canvasGroup.alpha = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : Singleton<CoinUI>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] UIBox coinUIBox;

    private void Awake()
    {
        WoonyMethods.Assert(this, (canvasGroup, nameof(canvasGroup)));
        Debug.Assert(canvasGroup != null, "canvasGroup is null", transform);

        canvasGroup.alpha = 0;
        coinUIBox.gameObject.SetActive(false);
    }

    public void UpdateAmount(int value)
    {
        coinUIBox.UpdateAmount(value);
        RefreshUI();
    }

    private void RefreshUI()
    {
        bool isAbleToShow = coinUIBox.gameObject.activeSelf;

        if (isAbleToShow)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }
}

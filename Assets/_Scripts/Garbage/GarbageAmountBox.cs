using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarbageAmountBox : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI count;

    public void Initialize()
    {
        WoonyMethods.Assert(this, (canvasGroup, nameof(canvasGroup)),
                                  (count, nameof(count)));
        Disappear();
    }

    private void Disappear()
    {
        canvasGroup.alpha = 0;
    }

    public void UpdateAmount(int amount)
    {
        if (amount <= 0)
        {
            Disappear();
            return;
        }

        count.text = $"x{amount}";
    }
}

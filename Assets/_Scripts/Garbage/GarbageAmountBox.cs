using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarbageAmountBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI count;

    public void Initialize()
    {
        WoonyMethods.Assert(this, (canvasGroup, nameof(canvasGroup)),
                                  (count, nameof(count)));
        Disappear();
    }

    private void Appear()
    {
        canvasGroup.alpha = 1;
    }

    private void Disappear()
    {
        canvasGroup.alpha = 0;
    }

    private void OnUpdateAmount(int amount)
    {
        if (amount <= 0)
        {
            Disappear();
        }
        else
        {
            Appear();
        }
    }

    public void UpdateAmount(int amount)
    {
        OnUpdateAmount(amount);
        count.text = $"x{amount}";
    }
}

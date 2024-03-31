using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBox : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI amount;

    public void Initialize(Sprite iconSprite)
    {
        WoonyMethods.Assert(this,
            (iconSprite, nameof(iconSprite)),
            (iconImage, nameof(iconImage)),
            (amount, nameof(amount)));
        this.iconImage.sprite = iconSprite;
    }

    public void UpdateAmount(int value)
    {
        amount.text = value.ToString();

        if (value == 0)
        {
            gameObject.SetActive(false);
        }
        else if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}

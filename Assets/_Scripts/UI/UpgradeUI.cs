using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : Singleton<UpgradeUI>
{
    [SerializeField] Button bgButton;
    [SerializeField] Button upgradeButton;
    [SerializeField] Button closeButton;
    int price = 10;

    private void Awake()
    {
        WoonyMethods.Assert(this, (bgButton, nameof(bgButton)),
                                  (upgradeButton, nameof(upgradeButton)),
                                  (closeButton, nameof(closeButton)));

        upgradeButton.onClick.AddListener(() => OnClickUpgradeButton());
        closeButton.onClick.AddListener(() => OnClickCloseButton());
        bgButton.onClick.AddListener(OnClickCloseButton);
    }

    void OnClickUpgradeButton()
    {
        Player.Instance.OnUpgrade(price);
    }

    void OnClickCloseButton()
    {
        Close();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

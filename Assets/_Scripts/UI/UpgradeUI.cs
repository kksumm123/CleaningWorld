using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : Singleton<UpgradeUI>
{
    [SerializeField] private Button bgButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button closeButton;
    private int _price = 10;

    private void Awake()
    {
        WoonyMethods.Assert(this,
            (bgButton, nameof(bgButton)),
            (upgradeButton, nameof(upgradeButton)),
            (closeButton, nameof(closeButton)));

        upgradeButton.onClick.AddListener(() => OnClickUpgradeButton());
        closeButton.onClick.AddListener(() => OnClickCloseButton());
        bgButton.onClick.AddListener(OnClickCloseButton);
    }

    private void OnClickUpgradeButton()
    {
        Player.Instance.OnUpgrade(_price);
    }

    private void OnClickCloseButton()
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

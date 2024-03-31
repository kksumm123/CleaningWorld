using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private UpgradeShopPlayerDetector upgradeShopPlayerDetector;

    private void Start()
    {
        WoonyMethods.Assert(this, (upgradeShopPlayerDetector, nameof(upgradeShopPlayerDetector)));

        upgradeShopPlayerDetector.Initialize(
            onPlayerEnter: () => UIManager.Instance.ShowUpgradeUI(),
            onPlayerExit: () => UIManager.Instance.CloseUpgradeUI());
    }
}

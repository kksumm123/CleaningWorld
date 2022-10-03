using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] UpgradeShopPlayerDetector upgradeShopPlayerDetector;

    void Start()
    {
        WoonyMethods.Assert(this, (upgradeShopPlayerDetector, nameof(upgradeShopPlayerDetector)));

        upgradeShopPlayerDetector.Initialize(() => UIManager.Instance.ShowUpgradeUI(),
                                             () => UIManager.Instance.CloseUpgradeUI());
    }
}

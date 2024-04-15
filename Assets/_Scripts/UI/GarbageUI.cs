using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageUI : Singleton<GarbageUI>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UIBox baseUIBox;

    private Dictionary<GarbageType, UIBox> _garbageUIBoxMap = new();

    private void Awake()
    {
        WoonyMethods.Assert(this, (canvasGroup, nameof(canvasGroup)));
        Debug.Assert(canvasGroup != null, "canvasGroup is null", transform);

        canvasGroup.alpha = 0;
        baseUIBox.gameObject.SetActive(false);
    }

    private void GenerateUIBox(GarbageType garbageType)
    {
        var icon = GameResourcesManager.imageResources.GetImage(garbageType);

        var newUIBox = Instantiate(baseUIBox, baseUIBox.transform.parent);
        newUIBox.Initialize(icon);
        _garbageUIBoxMap[garbageType] = newUIBox;
    }

    public void UpdateAmount(GarbageType garbageType, int value)
    {
        if (!_garbageUIBoxMap.ContainsKey(garbageType))
        {
            GenerateUIBox(garbageType);
        }

        _garbageUIBoxMap[garbageType].UpdateAmount(value);
        RefreshUI();
    }

    private void RefreshUI()
    {
        var isAbleToShow = false;
        var disableGarbageUICount = 0;

        foreach (var item in _garbageUIBoxMap)
        {
            if (item.Value.gameObject.activeSelf)
            {
                isAbleToShow = true;
                break;
            }
            else
            {
                disableGarbageUICount++;
            }
        }

        if (disableGarbageUICount == _garbageUIBoxMap.Count)
        {
            isAbleToShow = false;
        }

        canvasGroup.alpha = isAbleToShow ? 1 : 0;
    }
}

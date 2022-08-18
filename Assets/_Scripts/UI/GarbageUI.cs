using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageUI : Singleton<GarbageUI>
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GarbageUIBox baseUIBox;

    Dictionary<GarbageType, GarbageUIBox> garbageUIBoxMap = new Dictionary<GarbageType, GarbageUIBox>();

    private void Awake()
    {
        WoonyMethods.Assert(this, (canvasGroup, nameof(canvasGroup)));
        Debug.Assert(canvasGroup != null, "canvasGroup is null", transform);

        canvasGroup.alpha = 0;
        baseUIBox.gameObject.SetActive(false);
    }

    private void GenerateUIBox(GarbageType garbageType)
    {
        var iconType = (IconType)Enum.Parse(enumType: typeof(IconType),
                                                        value: garbageType.ToString(),
                                                        ignoreCase: false);
        var icon = GameResourcesManager.Instance.GetIcon(iconType);

        var newUIBox = Instantiate(baseUIBox, baseUIBox.transform.parent);
        newUIBox.Initialize(icon);
        garbageUIBoxMap[garbageType] = newUIBox;
    }

    public void UpdateAmount(GarbageType garbageType, int value)
    {
        if (garbageUIBoxMap.ContainsKey(garbageType) == false)
        {
            GenerateUIBox(garbageType);
        }

        garbageUIBoxMap[garbageType].UpdateAmount(value);
        RefreshUI();
    }

    private void RefreshUI()
    {
        bool isAbleToShow = false;
        int disableGarbageUICount = 0;

        foreach (var item in garbageUIBoxMap)
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

        if (disableGarbageUICount == garbageUIBoxMap.Count)
        {
            isAbleToShow = false;
        }

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

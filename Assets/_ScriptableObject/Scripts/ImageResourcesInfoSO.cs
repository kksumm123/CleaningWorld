using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ImageResourcesInfoSO), menuName = "CleaningWorld/ImageResourcesInfoSO")]
public class ImageResourcesInfoSO : ScriptableObject
{
    [SerializeField] Sprite canIcon;
    [SerializeField] Sprite foodIcon;
    [SerializeField] Sprite glassIcon;
    [SerializeField] Sprite paperIcon;
    [SerializeField] Sprite plasticIcon;

    public Sprite GetIcon(IconType iconType)
    {
        switch (iconType)
        {
            case IconType.Can:
                return canIcon;
            case IconType.Food:
                return foodIcon;
            case IconType.Glass:
                return glassIcon;
            case IconType.Paper:
                return paperIcon;
            case IconType.Plastic:
                return plasticIcon;
            default:
                return null;
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastebasketLidController : MonoBehaviour
{
    [SerializeField] private Transform lid;
    [SerializeField] private LidOpenCloseInfo lidOpenInfo = new LidOpenCloseInfo(new Vector3(0, 105, -90), 0.2f);
    [SerializeField] private LidOpenCloseInfo lidCloseInfo = new LidOpenCloseInfo(new Vector3(0, 0, -90), 1);

    public void Initialize()
    {
        WoonyMethods.Assert(this, (lid, nameof(lid)));
    }

    public Tween Open()
    {
        lid.DOKill();
        return lid.DOLocalRotate(lidOpenInfo.rotation, lidOpenInfo.duration);
    }

    public void Close()
    {
        lid.DOKill();
        lid.DOLocalRotate(lidCloseInfo.rotation, lidCloseInfo.duration)
           .SetEase(Ease.OutBounce);
    }
}

[System.Serializable]
class LidOpenCloseInfo
{
    public Vector3 rotation;
    public float duration;

    public LidOpenCloseInfo(Vector3 rotation, float duration)
    {
        this.rotation = rotation;
        this.duration = duration;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageObject : RecycleObject
{
    public GarbageDetailType GarbageDetailType => garbageDetailType;

    [field: SerializeField] public GarbageType GarbageType { get; private set; }
    [SerializeField] GarbageDetailType garbageDetailType;
}

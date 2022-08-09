using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageObject : RecycleObject
{
    public GarbageType GarbageType => garbageType;

    [SerializeField] GarbageType garbageType;
}

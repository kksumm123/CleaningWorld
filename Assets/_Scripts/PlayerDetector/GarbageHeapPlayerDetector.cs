using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageHeapPlayerDetector : PlayerDetector
{
    int garbageTypesMaxNumber;

    protected override void OnStart()
    {
        var garbageTypes = Enum.GetNames(typeof(GarbageType));
        garbageTypesMaxNumber = garbageTypes.Length;
    }

    protected override void _OnTriggerEnter(Collider other)
    {
        GarbageType randomeType = (GarbageType)Random.Range(1, garbageTypesMaxNumber);

        var randomGarbage = FactoryManager.Instance
                                          .GetGarbageObject(randomeType,
                                                            transform.position);
        other.GetComponent<Player>().OnGarbageHeap(randomGarbage);
    }

    protected override void _OnTriggerExit(Collider other) { }
}

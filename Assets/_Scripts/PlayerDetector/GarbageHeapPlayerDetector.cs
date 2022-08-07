using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageHeapPlayerDetector : PlayerDetector
{
    protected override void _OnTriggerEnter()
    {
        Debug.Log("힣힣히 오줌발싸 !");
    }

    protected override void _OnTriggerExit() { }
}

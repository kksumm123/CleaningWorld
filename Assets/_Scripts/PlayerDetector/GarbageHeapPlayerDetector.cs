using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageHeapPlayerDetector : PlayerDetector
{
    private Action _onPlayerEnter;
    private Action _onPlayerExit;

    protected override void OnStart() { }

    public void Initialize(Action onPlayerEnter, Action onPlayerExit)
    {
        _onPlayerEnter = onPlayerEnter;
        _onPlayerExit = onPlayerExit;
    }

    protected override void _OnTriggerEnter(Collider other)
    {
        _onPlayerEnter();
    }

    protected override void _OnTriggerExit(Collider other)
    {
        _onPlayerExit();
    }
}

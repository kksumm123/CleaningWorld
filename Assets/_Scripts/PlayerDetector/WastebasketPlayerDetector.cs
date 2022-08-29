using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastebasketPlayerDetector : PlayerDetector
{
    Action onPlayerEnter;
    Action onPlayerExit;

    public void Initialize(Action onPlayerEnter, Action onPlayerExit)
    {
        this.onPlayerEnter = onPlayerEnter;
        this.onPlayerExit = onPlayerExit;
    }

    protected override void _OnTriggerEnter(Collider other)
    {
        onPlayerEnter?.Invoke();
    }

    protected override void _OnTriggerExit(Collider other)
    {
        onPlayerExit?.Invoke();
    }
}

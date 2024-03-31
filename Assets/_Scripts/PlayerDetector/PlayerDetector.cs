using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDetector : MonoBehaviour
{
    [SerializeField] private Collider mainCollider;

    private void Start()
    {
        Debug.Assert(mainCollider != null, $"mainCollider is null", transform);
        OnStart();
    }

    protected virtual void OnStart() { }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(AllTagAndLayer.Player)) return;

        _OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(AllTagAndLayer.Player)) return;

        _OnTriggerExit(other);
    }

    protected abstract void _OnTriggerEnter(Collider other);

    protected abstract void _OnTriggerExit(Collider other);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDetector : MonoBehaviour
{
    [SerializeField] Collider mainCollider;

    private void Start()
    {
        Debug.Assert(mainCollider != null, $"mainCollider is null", transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(AllTagAndLayer.Player) == false)
        {
            return;
        }

        _OnTriggerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(AllTagAndLayer.Player) == false)
        {
            return;
        }

        _OnTriggerExit();
    }

    protected abstract void _OnTriggerEnter();

    protected abstract void _OnTriggerExit();
}

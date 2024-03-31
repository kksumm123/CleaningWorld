using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BillboardObject : MonoBehaviour
{
    private Transform _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        transform.rotation = _mainCamera.rotation;
    }
}

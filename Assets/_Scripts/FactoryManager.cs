using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public static FactoryManager Instance;

    ObjectPoolSystem cubeObjectPool;
    [SerializeField] TestCube cubePrefab;

    public void Initialize()
    {
        Instance = this;

        cubeObjectPool = new ObjectPoolSystem(cubePrefab, 5, transform);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public TestCube GetTestCube()
    {
        return cubeObjectPool.Get() as TestCube;
    }
}

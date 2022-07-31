using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    ObjectPoolSystem cubeObjectPool;
    [SerializeField] TestCube cubePrefab;

    public void Initialize()
    {
        cubeObjectPool = new ObjectPoolSystem(cubePrefab, 5, transform);
    }

    public TestCube GetTestCube()
    {
        return cubeObjectPool.Get() as TestCube;
    }
}

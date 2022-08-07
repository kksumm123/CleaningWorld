using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    PrefabFactorySystem prefabFactorySystem = new PrefabFactorySystem();

    public void Initialize()
    {
        prefabFactorySystem.Initialize(transform);
    }
}

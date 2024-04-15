using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameResourcesManager : Singleton<GameResourcesManager>
{
    public static ImageResources imageResources = new();

    public async Task Initialize()
    {
        await imageResources.Initialize();
    }
}

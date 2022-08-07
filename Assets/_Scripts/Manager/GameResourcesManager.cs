using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourcesManager : Singleton<GameResourcesManager>
{
    GameResourcesSystem gameResourcesSystem = new GameResourcesSystem();

    public void Initialize()
    {
        gameResourcesSystem.Initialize();
    }
}

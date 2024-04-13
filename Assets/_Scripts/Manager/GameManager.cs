using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private readonly string mainIslandSceneName = "MainIsland";

    private async void Start()
    {
        GameResourcesManager.Instance.Initialize();
        await FactoryManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        LoadScene(mainIslandSceneName, LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneLoadSystem.LoadScene(sceneName, loadSceneMode);
    }
}

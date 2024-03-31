using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private readonly string mainIslandSceneName = "MainIsland";

    private void Awake()
    {
        GameResourcesManager.Instance.Initialize();
        FactoryManager.Instance.Initialize();
        UIManager.Instance.Initialize();
    }

    private void Start()
    {
        LoadScene(mainIslandSceneName, LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneLoadSystem.LoadScene(sceneName, loadSceneMode);
    }
}

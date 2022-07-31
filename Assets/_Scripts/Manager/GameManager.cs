using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] FactoryManager factoryManager;
    [SerializeField] UIManager uiManager;

    private void Awake()
    {
        factoryManager.Initialize();
        uiManager.Initialize();
    }

    private void Start()
    {
        LoadScene("MainIsland", LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneLoadSystem.LoadScene(sceneName, loadSceneMode);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] FactoryManager factoryManager;
    [SerializeField] UIManager uiManager;

    private void Awake()
    {
        Instance = this;
        factoryManager.Initialize();
        uiManager.Initialize();
    }

    private void Start()
    {
        LoadScene("MainIsland", LoadSceneMode.Additive);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneLoadSystem.LoadScene(sceneName, loadSceneMode);
    }
}

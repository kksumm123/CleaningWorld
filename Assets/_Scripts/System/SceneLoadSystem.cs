using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SceneLoadSystem
{
    private static bool IsAbleToLoadScene = true;

    private static void OnLoadedScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        // sceneLoaded 이벤트 실행 시점은 Awake()보다 느림
        // SceneLoad는 최소 Start()에서 실행해주자
        SceneManager.sceneLoaded -= OnLoadedScene;
        SceneManager.SetActiveScene(scene);

        // 로드가 완료 되면 할 행동
        IsAbleToLoadScene = true;
    }

    private static IEnumerator AsynLoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        if (IsAbleToLoadScene == false)
        {
            yield break;
        }

        IsAbleToLoadScene = false;

        SceneManager.sceneLoaded += OnLoadedScene;
        var process = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        yield return new WaitUntil(() => process.isDone);
        process.allowSceneActivation = true;
    }

    public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        // 잘못 사용하기도 어렵게 만들어라.
        // 외부에서 코루틴을 직접 실행하게 하지 말고
        // 내부에서 래핑하고 실행하도록 만들자
        GameManager.Instance.StartCoroutine(AsynLoadScene(sceneName, loadSceneMode));
    }
}

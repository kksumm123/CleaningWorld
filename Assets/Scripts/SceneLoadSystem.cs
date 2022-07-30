using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SceneLoadSystem
{
    static bool isAbleToLoadScene = true;

    private static void OnLoadedScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= OnLoadedScene;
        // 로드가 완료 되면 할 행동
        isAbleToLoadScene = true;
    }

    private static IEnumerator AsynLoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        if (isAbleToLoadScene == false)
        {
            yield break;
        }

        isAbleToLoadScene = false;

        SceneManager.sceneLoaded += OnLoadedScene;
        var process = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        yield return new WaitUntil(() => process.isDone);
    }

    public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        // 잘못 사용하기도 어렵게 만들어라.
        // 외부에서 코루틴을 직접 실행하게 하지 말고
        // 내부에서 래핑하고 실행하도록 만들자
        GameManager.Instance.StartCoroutine(AsynLoadScene(sceneName, loadSceneMode));
    }
}

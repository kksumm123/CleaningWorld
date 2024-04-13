using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class WoonyMethods
{
    /// <summary>
    /// 예시 : WoonyMethods.Asserts(this, (variable, nameof(variable)));
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="objs"></param>
    public static void Assert<T>(T t, params (UnityEngine.Object, string)[] objs) where T : MonoBehaviour
    {
#if UNITY_EDITOR
        foreach (var obj in objs)
        {
            if (obj.Item1 == null)
            {
                AlertError($"{t.transform.name} : {obj.Item2} is null", t.transform);
            }
        }
#else
        foreach (var obj in objs)
        {
            Debug.Assert(obj.Item1 != null, $"{t.transform.name} : {obj.Item2} is null", t.transform);
        }
#endif
    }

    public static void Assert(string caller, params (UnityEngine.Object, string)[] objs)
    {
#if UNITY_EDITOR
        foreach (var obj in objs)
        {
            if (obj.Item1 == null)
            {
                AlertError($"{caller} : {obj.Item2} is null");
            }
        }
#else
        foreach (var obj in objs)
        {
            Debug.Assert(obj.Item1 != null, $"{caller} : {obj.Item2} is null");
        }
#endif
    }

    /// <summary>
    /// bool 조건이 false라면 메시지를 띄웁니다
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="objs"></param>
    public static void Assert<T>(T t, params (bool expressionResult, string message)[] objs) where T : MonoBehaviour
    {
#if UNITY_EDITOR
        foreach (var obj in objs)
        {
            if (obj.expressionResult == false)
            {
                AlertError($"{t.transform.name} : {obj.message}", t.transform);
            }
        }
#else
        foreach (var obj in objs)
        {
            if (obj.expressionResult == false)
            {
                Debug.Assert(obj.expressionResult, $"{t.transform.name} : {obj.message}", t.transform);
            }
        }
#endif
    }

    public static void Assert(string caller, params (bool expressionResult, string message)[] objs)
    {
#if UNITY_EDITOR
        foreach (var obj in objs)
        {
            if (obj.expressionResult == false)
            {
                AlertError($"{caller} : {obj.message}");
            }
        }
#else
        foreach (var obj in objs)
        {
            if (obj.expressionResult == false)
            {
                Debug.Assert(obj.expressionResult, $"{caller} : {obj.message}");
            }
        }
#endif
    }

    public static void AlertError(string v)
    {
        Debug.LogError($"우니 : {v}");
#if UNITY_EDITOR
        EditorUtility.DisplayDialog("우니 알림", $"우니 : {v}", "오케이 ~");
#endif
    }

    public static void AlertError(string v, Transform transform)
    {
        Debug.LogError($"우니 : {v}", transform);
#if UNITY_EDITOR
        EditorUtility.DisplayDialog("우니 알림", $"우니 : {v}", "오케이 ~");
#endif
    }
}

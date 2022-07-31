using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Joystick Joystick => joystick;

    public static UIManager Instance;

    [SerializeField] Joystick joystick;

    public void Initialize()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Joystick Joystick => joystick;
    [SerializeField] Joystick joystick;

    public void Initialize() { }

    public void UpdateGarbageAmount(GarbageType garbageType, int value)
    {
        GarbageUI.Instance.UpdateAmount(garbageType, value);
    }

    public void UpdateCoinAmout(int value)
    {
        CoinUI.Instance.UpdateAmount(value);
    }
}

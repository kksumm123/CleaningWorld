using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Joystick Joystick => joystick;
    [SerializeField] Joystick joystick;

    public void Initialize() { }
}

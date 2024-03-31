using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMoveSystem
{
    private Joystick _joystick;
    private Player _player;

    [SerializeField] private float speed = 5;
    [SerializeField] private float lerpValue = 0.25f;
    private Vector3 _direction;
    private Vector3 _moveResult;

    public void Initialize(Player player)
    {
        this._player = player;
        _joystick = UIManager.Instance.Joystick;
    }

    public void Move()
    {
        if (_joystick.IsDrag == false)
        {
            return;
        }

        _direction.x = _joystick.Horizontal;
        _direction.z = _joystick.Vertical;
        _direction.Normalize();

        _player.transform.forward = Vector3.Lerp(_player.transform.forward, _direction, lerpValue);

        _moveResult = speed * Time.deltaTime * _direction;
        _player.transform.Translate(_moveResult, Space.World);
    }
}

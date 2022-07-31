using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMoveSystem
{
    Joystick joystick;
    Player player;

    [SerializeField] float speed = 5;
    [SerializeField] float lerpValue = 0.25f;
    Vector3 direction;
    Vector3 moveResult;

    public void Initialize(Player player)
    {
        this.player = player;
        joystick = UIManager.Instance.Joystick;
    }

    public void Move()
    {
        if (joystick.IsDrag == false)
        {
            return;
        }

        direction.x = joystick.Horizontal;
        direction.z = joystick.Vertical;
        direction.Normalize();

        player.transform.forward = Vector3.Lerp(player.transform.forward,
                                                direction,
                                                lerpValue);

        moveResult = speed * Time.deltaTime * direction;
        player.transform.Translate(moveResult, Space.World);
    }
}

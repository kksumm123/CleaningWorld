using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] PlayerMoveSystem playerMoveSystem = new PlayerMoveSystem();

    private void Start()
    {
        playerMoveSystem.Initialize(this);
    }

    private void FixedUpdate()
    {
        playerMoveSystem.Move();
    }
}

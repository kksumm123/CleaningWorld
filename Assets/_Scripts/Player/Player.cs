using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] PlayerMoveSystem playerMoveSystem = new PlayerMoveSystem();
    [SerializeField] PlayerGarbageStackSystem playerGarbageStackSystem = new PlayerGarbageStackSystem();

    private void Start()
    {
        playerMoveSystem.Initialize(this);
        playerGarbageStackSystem.Initialize(this);
    }

    private void FixedUpdate()
    {
        playerMoveSystem.Move();
    }

    public bool IsAbleToGetGarbage()
    {
        return playerGarbageStackSystem.IsAbleToGetGarbage();
    }

    public void OnGarbageHeap(GarbageObject garbageObject, float duration)
    {
        playerGarbageStackSystem.OnGarbageHeap(garbageObject, duration);
    }
}

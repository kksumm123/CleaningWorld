using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] PlayerMoveSystem playerMoveSystem = new PlayerMoveSystem();
    [SerializeField] PlayerGarbageStackSystem playerGarbageStackSystem = new PlayerGarbageStackSystem();
    PlayerCoinSystem coinSystem = new PlayerCoinSystem();

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

    public bool IsAbleToPopGarbage(GarbageType garbageType)
    {
        return playerGarbageStackSystem.IsAbleToPopGarbage(garbageType);
    }

    public void OnGarbageHeap(GarbageObject garbageObject, float duration)
    {
        playerGarbageStackSystem.OnGarbageHeap(garbageObject, duration);
    }

    public (bool isContained, GarbageObject garbageObject) OnWastebasket(GarbageType garbageType)
    {
        if (playerGarbageStackSystem.IsAbleToPopGarbage(garbageType) == false)
        {
            return (false, null);
        }

        return playerGarbageStackSystem.OnWastebasket(garbageType);
    }

    public void AddCoin(int value)
    {
        coinSystem.AddCoin(value);
    }

    public void SubCoin(int value)
    {
        coinSystem.SubCoin(value);
    }

    public bool IsSubable(int value)
    {
        return coinSystem.IsSubable(value);
    }
}

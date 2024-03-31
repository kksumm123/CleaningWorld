using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private PlayerMoveSystem playerMoveSystem = new();
    [SerializeField] private PlayerGarbageStackSystem playerGarbageStackSystem = new();
    private PlayerCoinSystem _coinSystem = new();

    private void Start()
    {
        playerMoveSystem.Initialize(this);
        playerGarbageStackSystem.Initialize(this);
        _coinSystem.Initialize();
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

    public void OnUpgrade(int price)
    {
        if (!_coinSystem.IsSubable(price)) return;

        _coinSystem.SubCoin(price);
        playerGarbageStackSystem.OnUpgrade();
    }

    public void OnGarbageHeap(GarbageObject garbageObject, float duration)
    {
        playerGarbageStackSystem.OnGarbageHeap(garbageObject, duration);
    }

    public (bool isContained, GarbageObject garbageObject) OnWastebasket(GarbageType garbageType)
    {
        if (playerGarbageStackSystem.IsAbleToPopGarbage(garbageType) == false)
        {
            return (isContained: false, garbageObject: null);
        }

        return playerGarbageStackSystem.OnWastebasket(garbageType);
    }

    public void AddCoin(int value)
    {
        _coinSystem.AddCoin(value);
    }

    public void SubCoin(int value)
    {
        _coinSystem.SubCoin(value);
    }

    public bool IsSubable(int value)
    {
        return _coinSystem.IsSubable(value);
    }
}

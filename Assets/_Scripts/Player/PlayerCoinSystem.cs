using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinSystem
{
    static readonly string LOAD_KEY = "CoinAmout";
    int coin;

    public void Initialize()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(LOAD_KEY))
        {
            coin = PlayerPrefs.GetInt(LOAD_KEY, 0);
        }

        UIManager.Instance.UpdateCoinAmout(coin);
    }

    void SaveData()
    {
        PlayerPrefs.SetInt(LOAD_KEY, coin);
    }

    void UpdateCoin(int value)
    {
        coin += value;
        UIManager.Instance.UpdateCoinAmout(coin);
        SaveData();
    }

    public void AddCoin(int value)
    {
        if (value < 0)
        {
            Debug.Log("AddCoin() : 음수가 들어왔다");
            value = Math.Abs(value);
        }

        UpdateCoin(value);
    }

    public void SubCoin(int value)
    {
        if (value < 0)
        {
            Debug.Log("SubCoin() : 음수가 들어왔다");
            value = Math.Abs(value);
        }

        UpdateCoin(-value);
    }

    public bool IsSubable(int value)
    {
        return coin >= value;
    }
}

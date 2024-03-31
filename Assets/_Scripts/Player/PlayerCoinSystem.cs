using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinSystem
{
    private static readonly string LOAD_KEY = "CoinAmout";
    private int _coin;

    public void Initialize()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(LOAD_KEY))
        {
            _coin = PlayerPrefs.GetInt(LOAD_KEY, 0);
        }

        UIManager.Instance.UpdateCoinAmout(_coin);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(LOAD_KEY, _coin);
    }

    private void UpdateCoin(int value)
    {
        _coin += value;
        UIManager.Instance.UpdateCoinAmout(_coin);
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
        return _coin >= value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }
    public Action RunStarted;
    public Action CurrenciesChanged;

    //settings
    [SerializeField] GameObject _playerPrefab = null; 


    //state
    GameObject _player;
    public GameObject Player => _player;

    [SerializeField] int _energy;
    [SerializeField] int _emeralds;
    [SerializeField] int _framing;
    public int Energy => _energy;
    public int Emerald => _emeralds;
    public int Framing => _framing;


    private void Awake()
    {
        Instance = this;
        _energy = 100;
        _emeralds = 0;
        _framing = 0;
    }



    public void StartRun()
    {
        if (_player) Destroy(_player);


        Vector2 startPos = TilesController.Instance.GetRandomStartPos();
        _player = Instantiate(_playerPrefab, startPos, Quaternion.identity);

        //Instantly excavate the place where player started
        TilesController.Instance.PushNewRandomTileValues();

        RunStarted?.Invoke();
    }

    public void StartNextMine()
    {
        TilesController.Instance.PushNewRandomTileValues();
    }
    
    public void GainEmerald(int count)
    {
        _emeralds += count;
        CurrenciesChanged?.Invoke();
    }

    public void SpendEnergy(int amountToSpend)
    {
        //Debug.Log($"spent {amountToSpend} energy");
        _energy -= amountToSpend;
        CurrenciesChanged?.Invoke();
    }

    public void GainEnergy(int count)
    {
        _energy += count;
        CurrenciesChanged?.Invoke();
    }

    public void SpendFraming(int framingToSpend)
    {
        _framing -= framingToSpend;
        CurrenciesChanged?.Invoke();
    }

    public void GainFraming (int count)
    {
        _framing += count;
        CurrenciesChanged?.Invoke();
    }


}

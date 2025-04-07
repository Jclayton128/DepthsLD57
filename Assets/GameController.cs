using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }
    public Action RunStarted;
    public Action EmeraldsIncreased;
    public Action EnergyIncreased;
    public Action<bool> EnergyDecreased; 
    public Action FramingIncreased;
    public Action FramingDecreased;

    //refs
    [SerializeField] GameObject _titleHolder = null;
    [SerializeField] GameObject _endgameHolder = null;
    [SerializeField] TextMeshPro _endgameEmeraldCountTMP = null;
    [SerializeField] TextMeshPro _endgameCauseTMP = null;
    [SerializeField] TextMeshPro _endgameTipTMP = null;
 
    //settings
    [SerializeField] GameObject _playerPrefab = null;
    string _endgameTip_Collapse = "Don't dig away too much!";
    string _endgameTip_Starve = "Don't run out of energy!";


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
        _endgameHolder.SetActive(false);
        _titleHolder.SetActive(true);
    }

    

    public void StartRun()
    {
        if (_player) Destroy(_player);

        CameraController.Instance.MoveToGameplay();

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
        EmeraldsIncreased?.Invoke();
    }

    public void SpendEnergy(int amountToSpend)
    {
        //Debug.Log($"spent {amountToSpend} energy");
        _energy -= amountToSpend;

        if (amountToSpend > 1) EnergyDecreased?.Invoke(false);
        else EnergyDecreased?.Invoke(true);

        if (_energy <= 0)
        {
            ExecuteLoss(false);
        }
    }

    public void GainEnergy(int count)
    {
        _energy += count;
        EnergyIncreased?.Invoke();
    }

    public void SpendFraming(int framingToSpend)
    {
        _framing -= framingToSpend;
        FramingDecreased?.Invoke();
    }

    public void GainFraming (int count)
    {
        _framing += count;
        FramingIncreased?.Invoke();
    }
    
    public void ExecuteLoss(bool wasCollapse)
    {
        if (_player) Destroy(_player);
        _player = null;
        CameraController.Instance.MoveToTitle();
        _titleHolder.SetActive(false);
        _endgameEmeraldCountTMP.text = _emeralds.ToString();
        _endgameHolder.SetActive(true);

        if (wasCollapse)
        {
            _endgameCauseTMP.text = "Cause: Collapse";
            _endgameTipTMP.text = _endgameTip_Collapse;
        }
        else
        {
            _endgameCauseTMP.text = "Cause: Exhaustion";
            _endgameTipTMP.text = _endgameTip_Starve;
        }
    }
}

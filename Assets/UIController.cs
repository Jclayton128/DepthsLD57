using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    //scene refs
    [SerializeField] TotalizerDriver[] _rowTotalizers = null;
    [SerializeField] TotalizerDriver[] _columnTotalizers = null;
    [SerializeField] TextMeshPro _emeraldTMP = null;
    [SerializeField] TextMeshPro _energyTMP = null;
    [SerializeField] TextMeshPro _framingTMP = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TilesController.Instance.ValuesChanged += SetNewTotalizerValues;
        GameController.Instance.RunStarted += HandleRunStarted;
    }

    private void HandleRunStarted()
    {
        GameController.Instance.CurrenciesChanged += SetCurrencies;
        SetCurrencies();
    }

    private void SetCurrencies()
    {
        _emeraldTMP.text = GameController.Instance.Emerald.ToString();
        _energyTMP.text = GameController.Instance.Energy.ToString();
        _framingTMP.text = GameController.Instance.Framing.ToString();
    }

    private void SetNewTotalizerValues()
    {
        for (int i = 0; i < _rowTotalizers.Length; i++)
        {
            _rowTotalizers[i].SetValue(TilesController.Instance.GetTotalInRow(i));
        }

        for (int i = 0; i < _columnTotalizers.Length; i++)
        {
            _columnTotalizers[i].SetValue(TilesController.Instance.GetTotalInColumn(i));
        }
    }
}

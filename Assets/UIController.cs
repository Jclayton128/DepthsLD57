using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI.Core;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    //scene refs
    [SerializeField] TotalizerDriver[] _rowTotalizers = null;
    [SerializeField] TotalizerDriver[] _columnTotalizers = null;
    [SerializeField] Febucci.UI.TextAnimator _emeraldTMP = null;
    [SerializeField] Febucci.UI.TextAnimator _energyTMP = null;
    [SerializeField] Febucci.UI.TextAnimator _framingTMP = null;

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
        GameController.Instance.EmeraldsIncreased += HandleEmeraldIncrease;

        GameController.Instance.EnergyIncreased += HandleEnergyIncreased;
        GameController.Instance.EnergyDecreased += HandleEnergyDecreased;

        GameController.Instance.FramingIncreased += HandleFramingDecreased;
        GameController.Instance.FramingDecreased += HandleFramingDecreased;

        HandleEmeraldIncrease();

        HandleEnergyIncreased();
        HandleFramingIncreased();

        HandleEnergyDecreased(false);
        HandleFramingDecreased();
    }

    private void HandleEmeraldIncrease()
    {
        _emeraldTMP.SetText("{size}" + GameController.Instance.Emerald.ToString() + "{/size}", false);
    }

    private void HandleEnergyIncreased()
    {
        _energyTMP.SetText("{size}" + GameController.Instance.Energy.ToString() + "{/size}", false);
    }

    private void HandleFramingIncreased()
    {
        _framingTMP.SetText("{size}" + GameController.Instance.Framing.ToString() + "{/size}", false);
    }

    private void HandleEnergyDecreased(bool isMoreThanOne)
    {
        if (isMoreThanOne) _energyTMP.SetText("{fade}" + GameController.Instance.Energy.ToString() + "{/fade}", false);
        else _energyTMP.SetText(GameController.Instance.Energy.ToString(), false);
    }

    private void HandleFramingDecreased()
    {
        _framingTMP.SetText("{fade}" + GameController.Instance.Framing.ToString() + "{/fade}", false);
    }

    private void SetNewTotalizerValues()
    {
        for (int i = 0; i < _rowTotalizers.Length; i++)
        {
            int rowTotal = TilesController.Instance.GetTotalInRow(i);
            _rowTotalizers[i].SetValue(rowTotal);
        }

        for (int i = 0; i < _columnTotalizers.Length; i++)
        {
            int colTotal = TilesController.Instance.GetTotalInColumn(i);
            _columnTotalizers[i].SetValue(colTotal);
        }
    }
}

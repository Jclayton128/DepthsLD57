using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    //scene refs
    [SerializeField] TotalizerDriver[] _rowTotalizers = null;
    [SerializeField] TotalizerDriver[] _columnTotalizers = null;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TilesController.Instance.ValuesChanged += SetNewTotalizerValues;
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

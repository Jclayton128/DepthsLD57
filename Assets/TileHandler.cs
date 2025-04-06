using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileHandler : MonoBehaviour
{
    //refs
    [SerializeField] TextMeshPro _tmp = null;


    //state
    int _row;
    int _col;
    public int Row;
    public int Col;

    int _tilevalue;
    public int TileValue => _tilevalue;
    bool _isValueRevealed;

    private void Awake()
    {
        HideValue();
        Col = Mathf.RoundToInt(transform.position.x);
        Row = Mathf.RoundToInt(transform.position.y);
    }



    #region Value

    public void SetTileValue(int value)
    {
        _tilevalue = value;
        _tmp.text = _tilevalue.ToString();
    }

    public void ShowValue()
    {
        _isValueRevealed = true;
        _tmp.enabled = _isValueRevealed;
    }

    public void HideValue()
    {
        _isValueRevealed = false;
        _tmp.enabled = _isValueRevealed;
    }

    #endregion
}

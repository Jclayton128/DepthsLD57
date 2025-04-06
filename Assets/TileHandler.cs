using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileHandler : MonoBehaviour
{
    //refs
    [SerializeField] TextMeshPro _tmp = null;

    //settings
    [SerializeField] Color _unexcavatedColor = Color.white;
    [SerializeField] Color _excavatedColor = Color.grey;

    //state
    int _row;
    int _col;
    public int Row;
    public int Col;

    int _tilevalue;
    public int TileValue => _tilevalue;
    bool _isValueRevealed;
    bool _isExcavated;

    private void Awake()
    {
        _isExcavated = false;
        HideValue();
        Col = Mathf.RoundToInt(transform.position.x);
        Row = Mathf.RoundToInt(transform.position.y);
    }



    #region Value

    public void SetTileValue(int value)
    {
        _tilevalue = value;
        _tmp.text = _tilevalue.ToString();
        _isExcavated = false;
    }

    public void ShowValue()
    {
        _isValueRevealed = true;
        _tmp.enabled = _isValueRevealed;

        if (!_isExcavated) _tmp.color = _unexcavatedColor;
        else _tmp.color = _excavatedColor;
    }



    public void HideValue()
    {
        _isValueRevealed = false;
        _tmp.enabled = _isValueRevealed;
    }

    #endregion

    #region Excavation

    public void ExcavateTile()
    {
        if (_isExcavated) return;
        _isExcavated = true;
        _tilevalue *= -1;
        _tmp.color = _excavatedColor;
    }

    #endregion
}

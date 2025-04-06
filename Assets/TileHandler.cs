using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TileHandler : MonoBehaviour
{
    public enum ResourceType { None, Energy, Emerald, Framing, Count}

    //refs
    [SerializeField] TextMeshPro _tmp = null;
    [SerializeField] SpriteRenderer _srBody = null;
    [SerializeField] SpriteRenderer _srResource = null;
    [SerializeField] SpriteRenderer _srCover = null;

    //settings
    [SerializeField] Color _unexcavatedColor = Color.white;
    [SerializeField] Color _excavatedColor = Color.grey;
    [SerializeField] Color _framedColor = Color.yellow;
    [SerializeField] Sprite[] _hiddenSprites = null;
    [SerializeField] Sprite[] _emptySprites = null;
    [SerializeField] Sprite[] _sandSprites = null;
    [SerializeField] Sprite[] _dirtSprites = null;
    [SerializeField] Sprite[] _rockSprites = null;

    [Header("Resource Sprites")]
    [SerializeField] Sprite _energySprite = null;
    [SerializeField] Sprite _emeraldSprite = null;
    [SerializeField] Sprite _framingSprite = null;

    //state
    int _row;
    int _col;
    public int Row => _row;
    public int Col => _col;

    [SerializeField] int _tilevalue;
    public int TileValue => _tilevalue;

    [SerializeField] ResourceType _resource = ResourceType.None;
    public ResourceType Resource => _resource;

    [SerializeField] bool _isTypeRevealed;
    [SerializeField] bool _isValueRevealed;
    [SerializeField] bool _isExcavated;

    private void Awake()
    {
        _isTypeRevealed = false;
        _isValueRevealed = false;
        _isExcavated = false;
        HideValue();
        _col = Mathf.RoundToInt(transform.position.x);
        _row = Mathf.RoundToInt(transform.position.y);

    }


    public void SetUpTileValue(int value, ResourceType resource)
    {
        HideValue();
        HideType();
        _tilevalue = value;
        _tmp.text = _tilevalue.ToString();
        _isExcavated = false;
        
        SetSpriteBasedOnValue();
        _resource = resource;
        SetResourceSprite();
    }

    private void SetResourceSprite()
    {
        switch (_resource)
        {
            case ResourceType.None:
                _srResource.sprite = null;
                break;
            case ResourceType.Energy:
                _srResource.sprite = _energySprite;
                break;
            case ResourceType.Emerald:
                _srResource.sprite = _emeraldSprite;
                break;
            case ResourceType.Framing:
                _srResource.sprite = _framingSprite;
                break;

        }
    }


    #region Value


    private void SetSpriteBasedOnValue()
    {
        if (_tilevalue == 0)
        {
            int rand = UnityEngine.Random.Range(0, _emptySprites.Length);
            _srBody.sprite = _emptySprites[rand];
        }
        else if ((_tilevalue > 0 && _tilevalue <= 3))
        {
            int rand = UnityEngine.Random.Range(0, _sandSprites.Length);
            _srBody.sprite = _sandSprites[rand];
        }
        else if ((_tilevalue > 3 && _tilevalue <= 6))
        {
            int rand = UnityEngine.Random.Range(0, _dirtSprites.Length);
            _srBody.sprite = _dirtSprites[rand];
        }
        else if ((_tilevalue > 6 && _tilevalue <= 9))
        {
            int rand = UnityEngine.Random.Range(0, _rockSprites.Length);
            _srBody.sprite = _rockSprites[rand];
        }
        else
        {
            Debug.LogWarning("Tile value doesn't map to a sprite");
        }

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

    #region Type

    public void ShowType()
    {
        _isTypeRevealed = true;
        _srCover.enabled = false;
    }

    public void HideType()
    {
        _isTypeRevealed = false;
        _srCover.enabled = true;
        int rand = UnityEngine.Random.Range(0, _hiddenSprites.Length);
        _srCover.sprite = _hiddenSprites[rand];
    }

    #endregion

    #region Excavation

    public void ExcavateTile()
    {
        if (_isExcavated) return;
        _isExcavated = true;


        if (_resource != ResourceType.None)
        {
            ExtractResource();
        }

        GameController.Instance.SpendEnergy(_tilevalue);

        _tilevalue *= -1;
        _tmp.color = _excavatedColor;

        ShowValue();
        int rand = UnityEngine.Random.Range(0, _emptySprites.Length);
        _srBody.sprite = _emptySprites[rand];
    }

    private void ExtractResource()
    {
        //Debug.Log($"Gained a {_resource}");

        switch (_resource)
        {
            case ResourceType.None:
                break;

            case ResourceType.Energy:
                GameController.Instance.GainEnergy(_tilevalue * 5);
                break;

            case ResourceType.Emerald:
                GameController.Instance.GainEmerald(_tilevalue * 1);
                break;

            case ResourceType.Framing:
                GameController.Instance.GainFraming(_tilevalue * 1);
                break;
        }

        _resource = ResourceType.None;
        SetResourceSprite();
    }

    #endregion

    #region Framing

    public void FrameTile()
    {
        _tilevalue = Mathf.Abs(_tilevalue);
        _tmp.color = _framedColor;
        //TODO depict framing on framed tile;
    }

    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesController : MonoBehaviour
{
    public static TilesController Instance { get; private set; }

    public Action ValuesChanged;

    //refs
    [SerializeField] GameObject[] _arrows = null;


    //settings
    [SerializeField] List<TileHandler> _tiles = null;
    [SerializeField] int _mapSize = 8;
    [SerializeField] int _minValue = 1;
    [SerializeField] int _maxValue = 9;
    [SerializeField] int _collapseThreshold_base = 10;

    //state
    public int CollapseThreshold => _collapseThreshold_base + _maxValue;
    TileHandler _lastExcavatedTile;

    private void Awake()
    {
        Instance = this;
        foreach (var arrow in _arrows)
        {
            arrow.SetActive(false);
        }
    }



    [ContextMenu("Generate New Tile Values")]

    public void PushNewRandomTileValues()
    {
        foreach (var tile in _tiles)
        {
            //TODO a better way to assign values?
            int rand = UnityEngine.Random.Range(_minValue, _maxValue + 1);

            TileHandler.ResourceType resource = TileHandler.ResourceType.None;
            int resrand = UnityEngine.Random.Range(0, 11);
            if ((resrand > 5 && resrand <= 7)) resource = TileHandler.ResourceType.Energy;
            if ((resrand > 7 && resrand <= 9)) resource = TileHandler.ResourceType.Framing;
            if ((resrand > 9 && resrand <= 10)) resource = TileHandler.ResourceType.Emerald;

            tile.SetUpTileValue(rand, resource);
        }

        foreach (var arrow in _arrows)
        {
            arrow.SetActive(false);
        }

        ValuesChanged?.Invoke();
    }

    public void ShowAllValues()
    {
        foreach (var tile in _tiles)
        {
            tile.ShowValue();
        }
    }

    public void HideAllValues()
    {
        foreach (var tile in _tiles)
        {
            tile.HideValue();
        }
    }

    public int GetTotalInRow(int row)
    {
        int runningValue = 0;
        foreach (var tile in _tiles)
        {
            if (tile.Row == row && tile.TileValue >= 0)
            {
                runningValue += tile.TileValue;
            }
        }

        if (runningValue > _collapseThreshold_base)
        {
            if (runningValue <= CollapseThreshold)
            {
                StartEmittingOnRow(row);
            }
        }
        else
        {
            int framingCost = Mathf.Abs(_lastExcavatedTile.TileValue) * 2;
            if (GameController.Instance.Framing >= framingCost)
            {
                Debug.Log($"framing! ");
                GameController.Instance.SpendFraming(framingCost);
                _lastExcavatedTile.ShowFrame();
                ValuesChanged?.Invoke();

            }
            else
            {
                CameraController.Instance.ShakeCamera(2);
                Debug.Log("Collapse!");
                GameController.Instance.ExecuteLoss();

            }
        }

        return runningValue;

    }

    public int GetTotalInColumn(int col)
    {
        int runningValue = 0;
        foreach (var tile in _tiles)
        {
            if (tile.Col == col && tile.TileValue >= 0)
            {
                runningValue += tile.TileValue;
            }
        }



        if (runningValue > _collapseThreshold_base)
        {
            if (runningValue <= CollapseThreshold)
            {
                StartEmittingOnCol(col);
            }
        }
        else
        {
            int framingCost = Mathf.Abs(_lastExcavatedTile.TileValue) * 2;
            if (GameController.Instance.Framing >= framingCost)
            {
                Debug.Log($"framing! ");
                GameController.Instance.SpendFraming(framingCost);
                _lastExcavatedTile.ShowFrame();
                ValuesChanged?.Invoke();

            }
            else
            {
                CameraController.Instance.ShakeCamera(2);
                Debug.Log("Collapse!");
                GameController.Instance.ExecuteLoss();
                //TODO trigger loss

            }
        }

        return runningValue;
    }

    public Vector2Int GetRandomStartPos()
    {
        //int rand = UnityEngine.Random.Range(0, _mapSize);
        return new Vector2Int(4, _mapSize - 1);
    }

    public bool CheckIfPositionIsValid(int destinationRow, int destinationCol)
    {
        bool isValid = true;
        
        if (destinationRow > _mapSize - 1 ||
            destinationCol < 0 || destinationCol > _mapSize - 1)
        {
            isValid = false;
        }

        //Debug.Log($"Row {destinationRow} and col {destinationCol} is valid? {isValid}");

        return isValid;
    }

    public void ProcessMove(int row, int col)
    {
        _lastExcavatedTile = null;
        foreach (var tile in _tiles)
        {
            if (tile.Row == row && tile.Col == col)
            {
                _lastExcavatedTile = tile;
                break;
            }
        }

        if (_lastExcavatedTile != null)
        {
            if (!_lastExcavatedTile.IsExcavated)
            {

                _lastExcavatedTile.ExcavateTile();

                TileHandler belowTile = GetTileHandlerAtPosition(row - 1, col);
                    if (belowTile) belowTile.ShowType();
                    else _arrows[col].SetActive(true);

                GetTileHandlerAtPosition(row + 1, col)?.ShowType();
                GetTileHandlerAtPosition(row, col + 1)?.ShowType();
                GetTileHandlerAtPosition(row, col - 1)?.ShowType();
                ValuesChanged?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning("Target Tile not found!");
        }
    }

    private TileHandler GetTileHandlerAtPosition(int row, int col)
    {
        foreach (var tile in _tiles)
        {
            if (tile.Row == row && tile.Col == col)
            {
                return tile;
            }
        }
        //Debug.LogWarning("no tile handler found at position");
        return null;
    }

    #region Emitting Particles
    public void StartEmittingOnRow(int row)
    {
        foreach (var tile in _tiles)
        {
            if (tile.Row == row)
            {
                tile.StartEmittingParticles();
            }
        }
    }

    public void StartEmittingOnCol(int col)
    {
        foreach (var tile in _tiles)
        {
            if (tile.Col == col)
            {
                tile.StartEmittingParticles();
            }
        }
    }

    public void StopEmittingEverywhere()
    {
        foreach (var tile in _tiles)
        {
            tile.StopClearEmittingParticles();
        }
    }
    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesController : MonoBehaviour
{
    public static TilesController Instance { get; private set; }

    public Action ValuesChanged;

    //settings
    [SerializeField] List<TileHandler> _tiles = null;
    [SerializeField] int _mapSize = 8;
    [SerializeField] int _minValue = 1;
    [SerializeField] int _maxValue = 9;
    [SerializeField] int _collapseThreshold_base = 10;

    //state
    public int CollapseThreshold => _collapseThreshold_base + _maxValue;

    private void Awake()
    {
        Instance = this;
    }



    [ContextMenu("Generate New Tile Values")]

    public void PushNewRandomTileValues()
    {
        foreach (var tile in _tiles)
        {
            int rand = UnityEngine.Random.Range(_minValue, _maxValue + 1);
            tile.SetTileValue(rand);
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

        if (runningValue < _collapseThreshold_base)
        {
            Debug.Log("Collapse!");
            //TODO trigger loss
            return 0;
        }
        else
        {
            return runningValue;
        }


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

        if (runningValue < _collapseThreshold_base)
        {
            Debug.Log("Collapse!");
            //TODO trigger loss
            return 0;
        }
        else
        {
            return runningValue;
        }
    }

    public Vector2Int GetRandomStartPos()
    {
        int rand = UnityEngine.Random.Range(0, _mapSize);
        return new Vector2Int(rand, _mapSize - 1);
    }

    public bool CheckIfPositionIsValid(int destinationRow, int destinationCol)
    {
        bool isValid = true;

        if (destinationRow > _mapSize - 1 ||
            destinationCol < 0 || destinationCol > _mapSize - 1)
        {
            isValid = false;
        }
            return isValid;
    }

    public void ProcessMove(int row, int col)
    {
        TileHandler targetTile = null;
        foreach (var tile in _tiles)
        {
            if (tile.Row == row && tile.Col == col)
            {
                targetTile = tile;
                break;
            }
        }

        if (targetTile != null)
        {
            targetTile.ExcavateTile();

            GetTileHandlerAtPosition(row - 1, col)?.ShowValue();
            GetTileHandlerAtPosition(row + 1, col)?.ShowValue();
            GetTileHandlerAtPosition(row, col + 1)?.ShowValue();
            GetTileHandlerAtPosition(row, col - 1)?.ShowValue();
            ValuesChanged?.Invoke();


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
        Debug.LogWarning("no tile handler found at position");
        return null;
    }
}

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


    private void Awake()
    {
        Instance = this;
    }



    [ContextMenu("Generate New Tile Values")]

    private void PushNewRandomTileValues()
    {
        foreach (var tile in _tiles)
        {
            int rand = UnityEngine.Random.Range(0, _mapSize + 1);
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
            if (tile.Row == row)
            {
                runningValue += tile.TileValue;
            }
        }
        return runningValue;
    }

    public int GetTotalInColumn(int col)
    {
        int runningValue = 0;
        foreach (var tile in _tiles)
        {
            if (tile.Col == col)
            {
                runningValue += tile.TileValue;
            }
        }
        return runningValue;
    }
}

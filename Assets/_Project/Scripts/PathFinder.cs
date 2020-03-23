﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    bool[,] grid;

    public static PathFinder instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetGrid(List<TileData> tileData , TwoDCoordinate boardSize)
    {
        grid = new bool[boardSize.x, boardSize.y];
        UpdateGrid(tileData, boardSize);
    }

    public void UpdateGrid(List<TileData> tileData, TwoDCoordinate boardSize)
    {
        int index = 0;
        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                grid[x, y] = tileData[index].isFilled;
                index++;
            }
        }
    }

    public List<TwoDCoordinate> GetPath(TwoDCoordinate agentPos , TwoDCoordinate targetPos)
    {
        TwoDCoordinate pos = new TwoDCoordinate();
        pos.x = agentPos.x / 2;
        pos.y = agentPos.y / 2;
        TwoDCoordinate target = new TwoDCoordinate();
        target.x = targetPos.x / 2;
        target.y = targetPos.y / 2;



        return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    public Tile[,] tiles { get; private set; }

    private void Awake()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        tiles = new Tile[_width, _height];
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity, transform);

                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                spawnedTile.Init(isOffset, x, y);
                tiles[x, y] = spawnedTile;
            }
        }

    }
}


//dictionary <string,bool>
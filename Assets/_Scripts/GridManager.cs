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
        tiles=new Tile [_width, _height];
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity, transform);

                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                spawnedTile.Init(isOffset, x, y);
                tiles[x, y] = spawnedTile;
                //or some other thing

            }

        }
        GenerateObstacles();
    }
    void AddObstacles(int x,int y, int obstacle)
    {
        tiles[x, y].obstacles.Add(obstacle);
    }
    void GenerateObstacles()
    {
        AddObstacles(1, 5, 4);
        AddObstacles(1, 4, 2);
        AddObstacles(1, 4, 4);
        AddObstacles(1, 3, 2);
        AddObstacles(1, 3, 3);

        AddObstacles(2, 3, 1);

        AddObstacles(3, 4, 3);
        AddObstacles(3, 3, 3);
        AddObstacles(3, 3, 4);
        AddObstacles(3, 2, 2);

        AddObstacles(4, 4, 1);
        AddObstacles(4, 3, 1);
        AddObstacles(4, 2, 3);

        AddObstacles(5, 4, 4);
        AddObstacles(5, 3, 2);
        AddObstacles(5, 2, 1);
    }

}


//dictionary <string,bool>
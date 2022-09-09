using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    public int x { get; private set; }
    public int y { get; private set; }

    public List<int> obstacles = new List<int>(0);
    //add 1 for left
    //add 2 for up
    // add 3 for right
    // add 4 for down
    // array enum

    public void Init(bool isOffset, int x, int y)
    {
        this.x = x;
        this.y = y;
        obstacles.Clear();
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

}

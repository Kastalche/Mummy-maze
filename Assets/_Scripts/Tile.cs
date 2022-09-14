using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    public int x { get; private set; }
    public int y { get; private set; }

    public void Init(bool isOffset, int x, int y)
    {
        this.x = x;
        this.y = y;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

}

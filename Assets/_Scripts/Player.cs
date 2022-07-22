using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PlayerMovement playerMovement;
    // public UnityEvent PlayerMoved;
    public Tile startPosition { get; private set; }

    void Awake()
    {
        //PlayerMoved = new UnityEvent();
    }
    private void Start()
    {
        startPosition = gridManager.tiles[1, 2];
        transform.position = startPosition.transform.position;
    }
    void Update()
    {
        playerMovement.GenerateMove();
    }



    public bool isAvailableFrom(Tile targetTile, Tile yourTile)
    {
        if (targetTile.obstacles.Count != 0)
        {
            if (targetTile.x != yourTile.x)   //if your x is diffrent
            {
                if (targetTile.x - yourTile.x == -1)    // u go left =>right
                {
                    if (targetTile.obstacles.Contains(3))
                        return false;
                    else return true;
                }
                else if (targetTile.x - yourTile.x == 1)   // righ => left
                {
                    if (targetTile.obstacles.Contains(1))
                        return false;
                    else return true;
                }
            }

            if (targetTile.y != yourTile.y)
            {
                if (targetTile.y - yourTile.y == 1)
                {
                    if (targetTile.obstacles.Contains(4))   // up => down
                        return false;
                    else return true;
                }
                else if (targetTile.y - yourTile.y == -1)
                {
                    if (targetTile.obstacles.Contains(2))  //down => up
                        return false;
                    else return true;
                }
            }
        }
        return true;
    }


}



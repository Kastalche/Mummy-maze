using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Player player;
    public UnityEvent PlayerMoved;
    public Tile startPosition { get; private set; }
    private void Start()
    {
        PlayerMoved = new UnityEvent();
    }

    void Update()
    {

    }
    public void GenerateMove()
    {
        var playerPos = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y];

        if (Input.GetKeyUp(KeyCode.D) && (int)player.transform.position.x + 1 < 6)
        {
            if (isAvailableFrom(gridManager.tiles[(int)playerPos.x + 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x + 1, (int)player.transform.position.y].transform.position;
                PlayerMoved?.Invoke();
                print("move");
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) && (int)player.transform.position.x - 1 > -1)
        {
            if (isAvailableFrom(gridManager.tiles[(int)playerPos.x - 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x - 1, (int)player.transform.position.y].transform.position;
                PlayerMoved?.Invoke();
                print("move");
            }
        }

        else if (Input.GetKeyUp(KeyCode.W) && (int)player.transform.position.y + 1 < 6)
        {
            if (isAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y + 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y + 1].transform.position;
                PlayerMoved?.Invoke();
                print("move");
            }
        }

        else if (Input.GetKeyUp(KeyCode.S) && (int)player.transform.position.y - 1 > -1)
        {
            if (isAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y - 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y - 1].transform.position;
                PlayerMoved?.Invoke();
                print("move");
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            PlayerMoved?.Invoke();
            print("move");
        }
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


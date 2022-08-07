using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    public UnityEvent ExplorerMoved;
    public Tile startPosition { get; private set; }
    private void Start()
    {
        ExplorerMoved = new UnityEvent();
    }
    public void GenerateExplorerMove(Character player)
    {
        var playerPos = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y];

        if (Input.GetKeyUp(KeyCode.D) && (int)player.transform.position.x + 1 < 6)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x + 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x + 1, (int)player.transform.position.y].transform.position;

                ExplorerMoved?.Invoke();
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) && (int)player.transform.position.x - 1 > -1)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x - 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x - 1, (int)player.transform.position.y].transform.position;

                ExplorerMoved?.Invoke();
            }
        }

        else if (Input.GetKeyUp(KeyCode.W) && (int)player.transform.position.y + 1 < 6)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y + 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y + 1].transform.position;

                ExplorerMoved?.Invoke();
            }
        }

        else if (Input.GetKeyUp(KeyCode.S) && (int)player.transform.position.y - 1 > -1)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y - 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y - 1].transform.position;

                ExplorerMoved?.Invoke();
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ExplorerMoved?.Invoke();
        }
    }
    public bool IsAvailableFrom(Tile targetTile, Tile yourTile)
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

    //------------------------------------------------------------------------------------
    //mummyMovmentMethods

    public void BotMoveHorizontally(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindPlayerTile().transform.position;

        if (mummyPos.x < playerPos.x)   //right
        {
            if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x + 1, (int)mummyPos.y], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x + 1, (int)mummyPos.y]);
            }
        }

        else   //left
        {
            if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x - 1, (int)mummyPos.y], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x - 1, (int)mummyPos.y]);
            }

        }
    }

    public void BotMoveVertically(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindPlayerTile().transform.position;

        if (mummyPos.y < playerPos.y) //up
        {
            if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y + 1], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y + 1]);
            }
        }

        else //down
        {
            if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y - 1], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y - 1]);
            }
        }
    }

    public void GoTo(Character mummy, Tile tile)
    {
        mummy.transform.position = tile.transform.position;
    }

    public Tile FindPlayerTile() //or players do it depending on how much player we have case
    {
         characters[0].TargetPosition=CompareExplores(characters[2],characters[3]).transform.position;
        //Compare player1 and player2 positions and check
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (gridManager.tiles[i, j].transform.position == targetPosition.transform.position)
                    return gridManager.tiles[i, j];
            }
        }
        return gridManager.tiles[(int)transform.position.x, (int)transform.position.y];
    }

    public bool MummyNotOnPlayersX(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindPlayerTile().transform.position;

        if (mummyPos.x != playerPos.x)
            return true;
        else return false;
    }

    public bool MummyNotOnPlayersY(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindPlayerTile().transform.position;

        if (mummyPos.y != playerPos.y)
            return true;
        else return false;
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

    public Character CompareExplores(Character mummy, Character player1, Character player2)
    {
        if ((Math.Abs((int)mummy.transform.position.x - (int)player1.transform.position.x) > Math.Abs((int)mummy.transform.position.x - (int)player2.transform.position.x)))
            return player2;
        else if (player1.startPosition.x == player2.startPosition.x)
            return player1;
        else
            return player1;
    }

    public void GeneratePlayerMummyMove(Character mummy)
    {

    }
}







//public void GenerateMoveSecondPlayer()
//{
//    var player = player2;
//    var playerPos = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y];
//    if (Input.GetKeyUp(KeyCode.D) && (int)player.transform.position.x + 1 < 6)
//    {
//        if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x + 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
//        {
//            player.transform.position = gridManager.tiles[(int)player.transform.position.x + 1, (int)player.transform.position.y].transform.position;
//            SecondPlayersMoved?.Invoke();
//        }
//    }
//    else if (Input.GetKeyUp(KeyCode.A) && (int)player.transform.position.x - 1 > -1)
//    {
//        if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x - 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
//        {
//            player.transform.position = gridManager.tiles[(int)player.transform.position.x - 1, (int)player.transform.position.y].transform.position;
//            SecondPlayersMoved?.Invoke();
//        }
//    }

//    else if (Input.GetKeyUp(KeyCode.W) && (int)player.transform.position.y + 1 < 6)
//    {
//        if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y + 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
//        {
//            player.transform.position = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y + 1].transform.position;
//            SecondPlayersMoved?.Invoke();
//        }
//    }

//    else if (Input.GetKeyUp(KeyCode.S) && (int)player.transform.position.y - 1 > -1)
//    {
//        if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y - 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
//        {
//            SecondPlayersMoved?.Invoke();
//        }
//    }

//    else if (Input.GetKeyUp(KeyCode.Space))
//    {
//        SecondPlayersMoved?.Invoke();
//    }
//}
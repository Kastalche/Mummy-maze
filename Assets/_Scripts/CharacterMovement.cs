using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] GameStateManagr gameManager;

    public UnityEvent ExplorerMoved;

    private void Start()
    {
        ExplorerMoved = new UnityEvent();
    }

    public void GeneratePlayerMove(Character player)
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

    public void GenerateBotMove(Character bot)
    {
        if (bot.isMummy)
        {
            if (MummyNotOnPlayersX(bot))
            {
                BotMoveHorizontally(bot);
                print("horizontal");
            }
            else
            {
                BotMoveVertically(bot);
                print("vertical");
            }

            if (MummyNotOnPlayersY(bot))
            {
                BotMoveVertically(bot);
            }
            else
            {
                BotMoveHorizontally(bot);
            }
        }
        else
        {
            BotMoveHorizontally(bot);
        }
        //don't forget to add a proper method for playerBotMove 
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

    public void GoTo(Character mummy, Tile tile)
    {
        mummy.transform.position = tile.transform.position;
    }

    public Tile FindExplorerTile(Character mummy)
    {
        var targetPlayer = CompareExplores(mummy);
        var target = targetPlayer.transform.position;
        return gridManager.tiles[(int)target.x, (int)target.y];
        //if this row works I will buy myself a balkanche
    }

    public Character CompareExplores(Character mummy)
    {
        switch (gameManager.mode)
        {
            case GameModes.SinglePlayer:
                return gameManager.characters[1];

            case GameModes.Multiplayer:

                var player1 = gameManager.characters[1];
                var player2 = gameManager.characters[2];

                if ((Math.Abs((int)mummy.transform.position.x - (int)player1.transform.position.x) > Math.Abs((int)mummy.transform.position.x - (int)player2.transform.position.x)))
                    return player2;
                else if (player1.startPosition.x == player2.startPosition.x)
                    return player1;
                else
                    return player1;
            default:
                return gameManager.characters[1];
        }
    }

    #region Methods Used Only For Generating Bot Move
    public void BotMoveVertically(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindExplorerTile(mummy).transform.position;

        if (mummyPos.y < playerPos.y) //up
        {
            if (IsAvailableFrom(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y + 1], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y + 1]);
            }
        }

        else //down
        {
            if (IsAvailableFrom(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y - 1], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y - 1]);
            }
        }
    }
    public void BotMoveHorizontally(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindExplorerTile(mummy).transform.position;

        if (mummyPos.x < playerPos.x)   //right
        {
            if (IsAvailableFrom(gridManager.tiles[(int)mummyPos.x + 1, (int)mummyPos.y], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x + 1, (int)mummyPos.y]);
            }
        }

        else   //left
        {
            if (IsAvailableFrom(gridManager.tiles[(int)mummyPos.x - 1, (int)mummyPos.y], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
            {
                GoTo(mummy, gridManager.tiles[(int)mummyPos.x - 1, (int)mummyPos.y]);
            }

        }
    }
    public bool MummyNotOnPlayersX(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindExplorerTile(mummy).transform.position;

        if (mummyPos.x != playerPos.x)
            return true;
        else return false;
    }
    public bool MummyNotOnPlayersY(Character mummy)
    {
        var mummyPos = mummy.transform.position;
        var playerPos = FindExplorerTile(mummy).transform.position;

        if (mummyPos.y != playerPos.y)
            return true;
        else return false;
    }
    #endregion

}
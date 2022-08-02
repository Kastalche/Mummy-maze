using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class Mummy : MonoBehaviour

    {
        public Tile startPosition { get; private set; }
        [SerializeField] private GridManager gridManager;
        [SerializeField] private Player player1;
        [SerializeField] private Player player2;
        private Player player;

        public void Start()
        {
            startPosition = gridManager.tiles[5, 3];
            transform.position = startPosition.transform.position;
            player = player1;
        }
        public void MoveHorizontally()
        {
            var mummyPos = transform.position;
            var playerPos = FindPlayerTile().transform.position;

            if (mummyPos.x < playerPos.x)   //right
            {
                if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x + 1, (int)mummyPos.y], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
                {
                    GoTo(gridManager.tiles[(int)mummyPos.x + 1, (int)mummyPos.y]);
                }
            }

            else   //left
            {
                if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x - 1, (int)mummyPos.y], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
                {
                    GoTo(gridManager.tiles[(int)mummyPos.x - 1, (int)mummyPos.y]);
                }

            }
        }

        public void MoveVertically()
        {
            var mummyPos = transform.position;
            var playerPos = FindPlayerTile().transform.position;

            if (mummyPos.y < playerPos.y) //up
            {
                if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y + 1], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
                {
                    GoTo(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y + 1]);
                }
            }

            else //down
            {
                if (isAvailableFrom(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y - 1], gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y]))
                {
                    GoTo(gridManager.tiles[(int)mummyPos.x, (int)mummyPos.y - 1]);
                }
            }
        }

        public void GoTo(Tile tile)
        {
            transform.position = tile.transform.position;
        }

        public Tile FindPlayerTile()
        {
            ComparePlayers();
            //Compare player1 and player2 positions and check
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (gridManager.tiles[i, j].transform.position == player.transform.position)
                        return gridManager.tiles[i, j];
                }
            }
            return gridManager.tiles[(int)transform.position.x, (int)transform.position.y];
        }

        public bool MummyNotOnPlayersX()
        {
            var mummyPos = transform.position;
            var playerPos = FindPlayerTile().transform.position;

            if (mummyPos.x != playerPos.x)
                return true;
            else return false;
        }

        public bool MummyNotOnPlayersY()
        {
            var mummyPos = transform.position;
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

        public void ComparePlayers()
        {
            if ((Math.Abs((int)transform.position.x - (int)player1.transform.position.x) > Math.Abs((int)transform.position.x - (int)player2.transform.position.x)))
                player = player2;
            else if (player1.startPosition.x == player2.startPosition.x)
                player = player1;
            else
                player = player1;
        }



    }

}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] NetworkManager mng;

    public void GeneratePlayerMove(Character player)
    {
        var playerPos = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y];

        if (Input.GetKeyUp(KeyCode.D) && (int)player.transform.position.x + 1 < 6)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x + 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x + 1, (int)player.transform.position.y].transform.position;

                var playerJ = JsonUtility.ToJson(player);
                var tileJ = JsonUtility.ToJson(gridManager.tiles[(int)playerPos.x + 1, (int)playerPos.y]);

                mng.PlayerMove(new JSONObject(tileJ));
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) && (int)player.transform.position.x - 1 > -1)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x - 1, (int)playerPos.y], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x - 1, (int)player.transform.position.y].transform.position;
            }
        }

        else if (Input.GetKeyUp(KeyCode.W) && (int)player.transform.position.y + 1 < 6)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y + 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y + 1].transform.position;

            }
        }

        else if (Input.GetKeyUp(KeyCode.S) && (int)player.transform.position.y - 1 > -1)
        {
            if (IsAvailableFrom(gridManager.tiles[(int)playerPos.x, (int)playerPos.y - 1], gridManager.tiles[(int)playerPos.x, (int)playerPos.y]))
            {
                player.transform.position = gridManager.tiles[(int)player.transform.position.x, (int)player.transform.position.y - 1].transform.position;

            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {

        }
    }


    public void GoTo(Character mummy, Tile tile)
    {
        mummy.transform.position = tile.transform.position;
    }



}
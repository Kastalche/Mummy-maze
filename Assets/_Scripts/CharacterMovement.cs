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
        //var playerJ = JsonUtility.ToJson(player);
        //var tileJ = JsonUtility.ToJson(gridManager.tiles[(int)playerPos.x + 1, (int)playerPos.y]);

    }
    public void GoTo(Character mummy, Tile tile)
    {
        mummy.transform.position = tile.transform.position;
    }



}
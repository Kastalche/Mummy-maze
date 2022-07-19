using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagr : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Mummy mummy;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Canvas canvaseLose;

    void Start()
    {
        canvas.enabled = false;
        canvaseLose.enabled = false;
        player.PlayerMoved.AddListener(OnPlayerMoved);
    }

    void Update()
    {
        if (player.transform.position == mummy.transform.position)
        { 
            RestartGame();
            canvaseLose.enabled=true;
        }
        if (gridManager.tiles[5, 0].transform.position == player.transform.position)
        { 
            RestartGame();
            canvas.enabled = true;
        }
    }

    private void OnPlayerMoved()
    {
        
        canvas.enabled = false;
        canvaseLose.enabled = false;

             if (mummy.MummyNotOnPlayersX())
            {
                mummy.MoveHorizontally();
            }
            else
            {
                mummy.MoveVertically();
            }

            if (mummy.MummyNotOnPlayersY())
            {
                mummy.MoveVertically();
            }
            else
            {
                mummy.MoveHorizontally();
            }
       
    }

    public void RestartGame()
    {
        player.transform.position = player.startPosition.transform.position;
        mummy.transform.position=mummy.startPosition.transform.position;
    }

    
}

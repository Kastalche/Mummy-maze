using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManagr : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Mummy mummy;

    void Start()
    {
        player.PlayerMoved.AddListener(OnPlayerMoved);
    }

    void Update()
    {
        if (player.transform.position == mummy.transform.position)
            RestartGame();
    }

    private void OnPlayerMoved()
    {
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

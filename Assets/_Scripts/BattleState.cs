using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : IStateController
{
    GameStateManagr gameManager;
    public BattleController(GameStateManagr gm)
    {
        gameManager = gm;
    }
    public void Start()
    {
        if (gameManager.mode.ToString() == "SinglePlayer")
        {
            var explorer = gameManager.characters[1];
            gameManager.ExploresTurn();
            gameManager.CheckForGameEnd();
            gameManager.Transition(StateType.BattleState);
        }
        else if (gameManager.mode.ToString() == "MultiPlayer")
        {
            gameManager.ExploresTurn();
            gameManager.MummiesTurn();
            gameManager.Transition(StateType.BattleState);
        }
    }
    public void Destroy()
    {
        SceneManager.LoadScene("GameStart");
    }
}

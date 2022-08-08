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
            ExploresTurn();
            CheckForGameEnd();
            gameManager.Transition(GameStates.BattleState);
        }
        else if (gameManager.mode.ToString() == "MultiPlayer")
        {
            ExploresTurn();
            MummiesTurn();
            gameManager.Transition(GameStates.BattleState);
        }
    }
    public void Destroy()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ExploresTurn()
    {
        foreach (var character in gameManager.characters)
        {
            if (character.isMummy == false)
            {
                if (character.isBot)
                {
                    gameManager.playerMovement.GenerateBotMove(character);
                }
                else
                {
                    gameManager.playerMovement.GeneratePlayerMove(character);
                }
            }
        }
    }
    public void MummiesTurn()
    {
        foreach (var character in gameManager.characters)
        {
            if (character.isMummy == true && character.isBot == true)
                gameManager.playerMovement.GenerateBotMove(character);
            else if (character.isMummy == true && character.isBot == false)
                gameManager.playerMovement.GeneratePlayerMove(character);
        }
    }
    public void CheckForGameEnd()
    {
        //in progress
    }
}

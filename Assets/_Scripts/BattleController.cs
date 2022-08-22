using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : IStateController
{
    public SocketIOComponent socket;
    GameStateManagr gameManager;
    public BattleController(GameStateManagr gm)
    {
        gameManager = gm;
    }
    public void Start()
    {
        socket.On("ExploresTurn", ExploresTurn);
        socket.On("MummiesTurn", MummiesTurn);
        socket.On("CheckForGameEnd", CheckForGameEnd); // we will see if the client  is going to do this or onlt the server


        //if (gameManager.mode.ToString() == "SinglePlayer")
        //{
        //    ExploresTurn();
        //    CheckForGameEnd();
        //    gameManager.Transition(GameStates.BattleState);
        //}
        //else if (gameManager.mode.ToString() == "MultiPlayer")
        //{
        //    ExploresTurn();
        //    MummiesTurn();
        //    CheckForGameEnd();
        //    gameManager.Transition(GameStates.BattleState);
        //}
    }
    public void Destroy()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ExploresTurn(SocketIOEvent socketIOEvent)
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
    public void MummiesTurn(SocketIOEvent socketIOEvent)
    {
        foreach (var character in gameManager.characters)
        {
            if (character.isMummy == true && character.isBot == true)
                gameManager.playerMovement.GenerateBotMove(character);
            else if (character.isMummy == true && character.isBot == false)
                gameManager.playerMovement.GeneratePlayerMove(character);
        }
    }
    public void CheckForGameEnd(SocketIOEvent socketIOEvent)
    {//in progress
        //switch (gameManager.mode)
        //{
        //    case GameModes.SinglePlayer:
        //        if(gameManager.characters[1].transform.position==gameManager.playerMovement.gridManager.tiles[5,6].transform.position)
        //            ganm


        //}

    }


}

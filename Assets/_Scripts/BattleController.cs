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
        socket.On("CheckForGameEnd", CheckForGameEnd); // maybe the server needs to do this but for now i leave this here
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
    {
    }
}

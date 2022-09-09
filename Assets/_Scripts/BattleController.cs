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
        socket.On("ApplyMove", OnApplyMove);
        socket.On("ExploresTurn", OnExploresTurn);
        socket.On("MummiesTurn", OnMummiesTurn);
        socket.On("BatlleEnd", OnBattleEnd);
    }
    public void Destroy()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void OnExploresTurn(SocketIOEvent socketIOEvent)
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

    public void OnMummiesTurn(SocketIOEvent socketIOEvent)
    {
        foreach (var character in gameManager.characters)
        {
            if (character.isMummy == true && character.isBot == true)
                gameManager.playerMovement.GenerateBotMove(character);
            else if (character.isMummy == true && character.isBot == false)
                gameManager.playerMovement.GeneratePlayerMove(character);
        }
    }

    public void OnBattleEnd(SocketIOEvent socketIOEvent)
    {

    }

    public void OnApplyMove(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        Tile tile = JsonUtility.FromJson<Tile>(data);
    }
}

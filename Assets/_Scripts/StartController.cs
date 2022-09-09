using SocketIO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : IStateController
{
    GameStateManagr gameManager;

    //public SocketIOComponent socket;

    [SerializeField] private Canvas ExplorersWin;
    [SerializeField] private Canvas MummiesWin;
    public SocketIOComponent socket;

    public StartController(GameStateManagr gm)
    {
        gameManager = gm;
    }

    public void Start()
    {
        Debug.Log("StartController");

        DisableAllUI();
        AddCharacters();
        CharactersToStartPosition();

        gameManager.Transition(GameStates.BattleState);
    }


    public void Destroy()
    {

    }

    public void DisableAllUI()
    {
        ExplorersWin.enabled = false;
        MummiesWin.enabled = false;
    }

    public void CharactersToStartPosition()
    {
        foreach (var character in gameManager.characters)
        {
            character.GoToStartPosition();
        }
    }

    public void AddCharacters()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log(sceneName);
        if (sceneName == "SampleScene")
        {

        }
        else if (sceneName == "MultiPlayerScene")
        {

        }
    }

}

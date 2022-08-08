using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : IStateController
{
    GameStateManagr gameManager;

    public StartController(GameStateManagr gm)
    {
        gameManager = gm;
    }

    public void Start()
    {
        gameManager.DisableAllUI();
        gameManager.AddCharacters();
        gameManager.CharactersToStartPosition();
    }

    public void Destroy()
    {
        gameManager.Transition(GameStates.BattleState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : IStateController
{
    GameStateManagr gameManager;

    [SerializeField] private Canvas ExplorersWin;
    [SerializeField] private Canvas MummiesWin;

    public StartController(GameStateManagr gm)
    {
        gameManager = gm;
    }

    public void Start()
    {
        DisableAllUI();
        gameManager.AddCharacters();
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
}

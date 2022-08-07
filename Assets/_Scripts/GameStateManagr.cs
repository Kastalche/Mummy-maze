using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameModes { SinglePlayer, Multiplayer }
//public enum GameState { Explorers, Mummies, Check }
public enum StateType { StartState, BattleState, EndState }
public class GameStateManagr : MonoBehaviour
{
    public GameModes mode { get; private set; }
    private Character currentPlayer;
    [SerializeField] public List<Character> characters { get; private set; }

    [SerializeField] private GridManager gridManager;

    [SerializeField] private Canvas ExplorersWin;
    [SerializeField] private Canvas MummiesWin;
    [SerializeField] private PlayerMovement playerMovement;

    //private GameState gameState;

    //add player dying
    //case singlePlayer, case multiplear
    void Start()
    {
        state = new StartController(this);

    }

    void Update()
    {
        switch (mode)
        {
            case GameModes.SinglePlayer:
                {
                    var explorer = characters[1];
                    playerMovement.GenerateExplorerMove(explorer);
                    CheckForGameEnd();
                    break;
                }
            case GameModes.Multiplayer:
                {
                    switch (gameState)
                    {
                        case GameState.Explorers:
                            while (currentPlayer != null)
                            {
                                if (currentPlayer.isMummy == true)
                                {
                                    ExplorerMoved();
                                }
                                playerMovement.GenerateExplorerMove(currentPlayer);
                                gameState = GameState.Mummies;
                            }
                            break;

                        case GameState.Mummies:
                            foreach (var character in characters)
                            {
                                if (character.isMummy == false)
                                {
                                    playerMovement.GenerateExplorerMove(character);
                                }
                                else if (character.isMummy == true && character.isBot == true)
                                    GenerateBotMummyMove(character);
                                else (character.isMummy == true && character.isBot == false)
                                    playerMovement.GeneratePlayerMummyMove(character);
                            }

                            break;

                        case GameState.Check:
                            CheckForGameEnd();
                            break;
                    }
                    break;
                }
            default:
                break;
        }
    }
    public void Transition(StateType type)
    {
        state.Dispose();

        switch (type)
        {
            case StateType.StartState:
                state = new StartController(this);
                break;

            case StateType.BattleState:
                state = new BattleController(this);
                break;

            case StateType.EndState:
                state = new EndController(this);
                break;

            default:
                break;
        }
        state.Start();
    }

    private void ExplorerMoved()
    {
        if (mode == GameModes.SinglePlayer)
        {
            GenerateBotMummyMove(characters[0]);
        }
        else
        {
            while (currentPlayer != null)
                currentPlayer = characters[characters.IndexOf(currentPlayer) + 1];
        }

        ExplorersWin.enabled = false;
        MummiesWin.enabled = false;
    }


    public void CheckForGameEnd()
    {
        if (currentPlayer.transform.position == mummy.transform.position || currentPlayer.transform.position == mummy.transform.position)
        {
            RestartGame();
            MummiesWin.enabled = true;
        }
        else if (gridManager.tiles[5, 0].transform.position == currentPlayer.transform.position && gridManager.tiles[5, 0].transform.position == currentPlayer.transform.position)
        {
            RestartGame();
            ExplorersWin.enabled = true;
        }
        else
            gameState = GameState.Explorers;
    }

    public void GenerateBotMummyMove(Character mummy)
    {
        if (playerMovement.MummyNotOnPlayersX(mummy))
        {
            playerMovement.BotMoveHorizontally(mummy);
            print("horizontal");
        }
        else
        {
            playerMovement.BotMoveVertically(mummy);
            print("vertical");
        }

        if (playerMovement.MummyNotOnPlayersY(mummy))
        {
            playerMovement.BotMoveVertically(mummy);
        }
        else
        {
            playerMovement.BotMoveHorizontally(mummy);
        }
        gameState = GameState.Check;
    }

    public void ExploresTurn()
    {
        foreach (var character in characters)
        {
            if (character.isMummy == false)
                playerMovement.GenerateExplorerMove(character);
        }
    }

    public void MummiesTurn()
    {
        foreach (var character in characters)
        {
            if (character.isMummy == true && character.isBot == true)
                GenerateBotMummyMove(character);
            else if (character.isMummy == true && character.isBot == false)
                playerMovement.GeneratePlayerMummyMove(character);
        }
    }
    public void CharactersToStartPosition()
    {
        foreach (var character in characters)
        {
            character.GoToStartPosition();
        }
    }
    public void AddCharacters()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "SampleScene")
        {
            characters.Add(new Character { startPosition = gridManager.tiles[3, 5], isBot = true, isMummy = true });
            characters.Add(new Character { startPosition = gridManager.tiles[1, 2], isBot = false, isMummy = false, });
            mode = GameModes.SinglePlayer
        }
        else if (sceneName == "MultiPlayerScene")
        {
            mode = GameModes.Multiplayer;
            //one bot mummym and two players explorers
            characters.Add(new Character { startPosition = gridManager.tiles[1, 2], isBot = false, isMummy = false, });
            characters.Add(new Character { startPosition = gridManager.tiles[1, 4], isBot = false, isMummy = false, });
            characters.Add(new Character { startPosition = gridManager.tiles[1, 2], isBot = false, isMummy = true, });
        }
        currentPlayer = characters[0];
    }
    public void DisableAllUI()
    {
        ExplorersWin.enabled = false;
        MummiesWin.enabled = false;
    }
}










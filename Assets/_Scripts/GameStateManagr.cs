using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameModes { SinglePlayer, Multiplayer }
public enum GameState { Explorers, Mummies, Check }
public class GameStateManagr : MonoBehaviour
{
    private Character currentPlayer;
    [SerializeField] List<Character> characters;

    [SerializeField] private GridManager gridManager;

    [SerializeField] private Canvas ExplorersWin;
    [SerializeField] private Canvas MummiesWin;
    [SerializeField] private PlayerMovement playerMovement;

    private GameModes mode;
    private GameState gameState;

    //add player dying
    //case singlePlayer, case multiplear
    void Start()
    {
        currentPlayer = characters[0];
        ExplorersWin.enabled = false;
        MummiesWin.enabled = false;

        playerMovement.ExplorerMoved.AddListener(ExplorerMoved);

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "SampleScene")
        {//is it possible adding character to be in delegate;0
            //always first character in list is mummy
            characters.Add(new Character { startPosition = gridManager.tiles[3, 5], isBot = true, isMummy = true });
            characters.Add(new Character { startPosition = gridManager.tiles[1, 2], isBot = false, isMummy = false, });

            CharactersToStartPosition();

            mode = GameModes.SinglePlayer;

        }
        else if (sceneName == "MultiPlayerScene")
        {
            //one bot mummym and two players explorers
            characters.Add(new Character { startPosition = gridManager.tiles[1, 2], isBot = false, isMummy = false, });
            characters.Add(new Character { startPosition = gridManager.tiles[1, 4], isBot = false, isMummy = false, });
            characters.Add(new Character { startPosition = gridManager.tiles[1, 2], isBot = false, isMummy = true, });

            CharactersToStartPosition();

            mode = GameModes.Multiplayer;
        }
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
    public void RestartGame()
    {
        foreach (var character in characters)
        {
            character.transform.position = character.startPosition.transform.position;
        }
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
}









//player1.UnableMovement();
//                playerMovement.GenerateMoves(player2);
//                player2.UnableMovement();
//                player1.AbleMovement();
//                playerMovement.GenerateMove();
//                CheckForGameEnd();
//                break;

//public bool IsTheMummyNextToPlayerByX()
//{
//    if ((player.transform.position.x - 1 == mummy.transform.position.x && player.transform.position.y == mummy.transform.position.y) || (player.transform.position.x + 1 == mummy.transform.position.x && player.transform.position.y == mummy.transform.position.y))
//        return true;
//    else
//        return false;
//}
//public bool IsTheMummyNextToPlayerByY()
//{
//    if ((player.transform.position.y - 1 == mummy.transform.position.y && player.transform.position.x == mummy.transform.position.x) || (player.transform.position.y + 1 == mummy.transform.position.y && player.transform.position.x == mummy.transform.position.x))
//        return true;
//    else return false;
//}

//if (IsTheMummyNextToPlayerByX())
//{
//    mummy.transform.position = player.transform.position;
//    RestartGame();
//    canvaseLose.enabled = true;
//}

//else if (IsTheMummyNextToPlayerByY())
//{
//    mummy.transform.position = player.transform.position;
//    RestartGame();
//    canvaseLose.enabled = true;
//}
//else
//{
//------------------------------------------------------------------------------
//    roundState = RoundState.Firstplayer;
//                    switch (roundState)
//                    {
//                        case RoundState.Firstplayer:
//                            {
//                                player1.UnableMovement();
//                                playerMovement.GenerateMoves(player2);
//                                roundState = RoundState.SecondPlayer;
//                                break;
//                            }
//                        case RoundState.SecondPlayer:
//                            {
//    player2.UnableMovement();
//    player1.AbleMovement();
//    playerMovement.GenerateMove();
//    roundState = RoundState.Check;
//    break;
//}
//                        case RoundState.Check:
//                            {
//    CheckForGameEnd();
//    roundState = RoundState.Firstplayer;
//    break;
//}

//                    }
//                    break;
//}
//-------------------------------------------------------------------------------------------------
//
//playerMovement.GenerateMoveFirstPlayer();
//player1.UnableMovement();
//playerMovement.GenerateMoveSecondPlayer();
//player2.UnableMovement();
//player1.AbleMovement();
//playerMovement.GenerateMoveFirstPlayer();
//CheckForGameEnd();
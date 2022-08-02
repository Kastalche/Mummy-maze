using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { SinglePlayer, Multiplayer }
public enum RoundState { Firstplayer, SecondPlayer, Mummy, Check }
public class GameStateManagr : MonoBehaviour
{
    [SerializeField] private Player player1; // current plaeyr consept
    [SerializeField] private Player player2;
    [SerializeField] private Mummy mummy;

    [SerializeField] private GridManager gridManager;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Canvas canvaseLose;
    [SerializeField] private PlayerMovement playerMovement;

    private GameState state;
    private RoundState roundState;

    //case singlePlayer, case multiplear
    void Start()
    {
        canvas.enabled = false;
        canvaseLose.enabled = false;

        playerMovement.FirstPlayerMoved.AddListener(OnFirstPlayerMoved);
        playerMovement.SecondPlayersMoved.AddListener(OnSecondPlayersMoved);

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "SampleScene")
        {
            player1.startPosition = gridManager.tiles[1, 2];
            player1.transform.position = player1.startPosition.transform.position;

            state = GameState.SinglePlayer;

        }
        else if (sceneName == "MultiPlayerScene")
        {
            player1.startPosition = gridManager.tiles[1, 2];
            player1.transform.position = player1.startPosition.transform.position;

            player2.startPosition = gridManager.tiles[1, 4];
            player2.transform.position = player2.startPosition.transform.position;

            state = GameState.Multiplayer;
        }
    }

    void Update()
    {
        switch (state)
        {
            case GameState.SinglePlayer:
                {
                    playerMovement.GenerateMoveFirstPlayer();
                    CheckForGameEnd();
                    break;
                }
            case GameState.Multiplayer:
                {
                    switch (roundState)
                    {
                        case RoundState.Firstplayer:
                            print(roundState.ToString());
                            playerMovement.GenerateMoveFirstPlayer();

                            //change color of active player
                            player1.GetComponent<SpriteRenderer>().color = new Color(0.9811321f, 0.8928651f, 0.6238519f, 1);
                            player2.GetComponent<SpriteRenderer>().color = Color.white;

                            break;
                        case RoundState.SecondPlayer:
                            print(roundState.ToString());
                            playerMovement.GenerateMoveSecondPlayer();

                            //change color of active player
                            player1.GetComponent<SpriteRenderer>().color = Color.white;
                            player2.GetComponent<SpriteRenderer>().color = new Color(0.9811321f, 0.8928651f, 0.6238519f, 1);

                            break;

                        case RoundState.Mummy:
                            print(roundState.ToString());
                            GenerateMummyMove();
                            break;

                        case RoundState.Check:
                            print(roundState.ToString());
                            CheckForGameEnd();
                            break;
                    }
                    break;
                }
            default:
                break;
        }

    }

    private void OnFirstPlayerMoved()
    {
        if (state == GameState.SinglePlayer)
        {
            GenerateMummyMove();
        }
        else
        {
            roundState = RoundState.SecondPlayer;
        }

        canvas.enabled = false;
        canvaseLose.enabled = false;
    }
    private void OnSecondPlayersMoved()
    {
        roundState = RoundState.Mummy;
    }

    public void RestartGame()
    {
        player1.transform.position = player1.startPosition.transform.position;
        player2.transform.position = player2.startPosition.transform.position;
        mummy.transform.position = mummy.startPosition.transform.position;
    }

    public void CheckForGameEnd()
    {
        if (player1.transform.position == mummy.transform.position || player2.transform.position == mummy.transform.position)
        {
            RestartGame();
            canvaseLose.enabled = true;
        }
        else if (gridManager.tiles[5, 0].transform.position == player1.transform.position && gridManager.tiles[5, 0].transform.position == player2.transform.position)
        {
            RestartGame();
            canvas.enabled = true;
        }
        else
            roundState = RoundState.Firstplayer;
    }

    public void GenerateMummyMove()
    {
        if (mummy.MummyNotOnPlayersX())
        {
            mummy.MoveHorizontally();
            print("horizontal");
        }
        else
        {
            mummy.MoveVertically();
            print("vertical");
        }

        if (mummy.MummyNotOnPlayersY())
        {
            mummy.MoveVertically();
            print("vertical");
        }
        else
        {
            mummy.MoveHorizontally();
            print("horizontal");
        }
        roundState = RoundState.Check;
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
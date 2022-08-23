using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameModes { SinglePlayer, Multiplayer }
public enum GameStates { StartState, BattleState, EndState }

public class GameStateManagr : MonoBehaviour
{
    private Character currentPlayer;
    public GameModes mode { get; private set; }
    public static IStateController state;

    [SerializeField] public List<Character> characters { get; private set; }
    [SerializeField] public CharacterMovement playerMovement { get; private set; }
    [SerializeField] public GridManager gridManager;


    //add player dying

    private void Awake()
    {
        state = new StartController(this);
    }

    public void Transition(GameStates newState)
    {
        state.Destroy();

        switch (newState)
        {
            case GameStates.StartState:
                state = new StartController(this);
                break;

            case GameStates.BattleState:
                state = new BattleController(this);
                break;

            case GameStates.EndState:
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
            playerMovement.GenerateBotMove(characters[0]);
        }
        else
        {
            while (currentPlayer != null)
                currentPlayer = characters[characters.IndexOf(currentPlayer) + 1];
            playerMovement.GeneratePlayerMove(currentPlayer);
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
            mode = GameModes.SinglePlayer;
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
}










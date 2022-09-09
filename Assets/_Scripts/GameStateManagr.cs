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


    private void Awake()
    {
        // state = new StartController(this);
    }

    public void Transition(GameStates newState)
    {
        state?.Destroy();

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
}










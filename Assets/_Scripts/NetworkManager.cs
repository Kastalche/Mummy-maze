using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    //important things here, take a list of players id from surves, remove all bot logics, and state machine logics, and when server send player id and players draw.
    private List<string> playerIds = new List<string>();
    public static NetworkManager instance;
    public SocketIOComponent socket;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        socket.On("PlayerId", AddToPlayerList);
        socket.On("BattleState", OnBattleStart);
        socket.On("killExplorer", OnKilledExplorer);
        socket.On("ApplyMove", OnApplyMove);
        socket.On("ExploresTurn", OnExploresTurn);
        socket.On("MummiesTurn", OnMummiesTurn);
        socket.On("BatlleEnd", OnBattleEnd);
    }
    #region EmitToServerMethods
    public void PlaySingle()
    {
        socket.Emit("singlePlayer");

        print("emittted SinglePlayer");
    }

    public void PlayMulti()
    {
        socket.Emit("multiPlayer");
        print("emittted Multiplayer");
    }
    #endregion

    public void PlayerMove(JSONObject data)
    {
        socket.Emit("playerMove", data);
    }

    private void OnKilledExplorer(SocketIOEvent obj)
    {
        socket.Emit("KilledExplorer");
    }
    public void OnExploresTurn(SocketIOEvent socketIOEvent)
    {

    }

    public void OnMummiesTurn(SocketIOEvent socketIOEvent)
    {

    }

    public void OnBattleEnd(SocketIOEvent socketIOEvent)
    {

    }

    public void OnApplyMove(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        Tile tile = JsonUtility.FromJson<Tile>(data);
    }

    public void AddToPlayerList(SocketIOEvent socketIOEvent)
    {
        string data = socketIOEvent.data.ToString();
        playerIds.Add(data);
    }
    public void OnBattleStart(SocketIOEvent socketIOEvent)
    {

    }


}

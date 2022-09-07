using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
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
        socket.On("lobbyIsFull", OnFullLobby);
        socket.On("killExplorer", OnKilledExplorer);
    }

    void Update()
    {

    }
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

    public void PlayerMove(JSONObject tile)
    {
        socket.Emit("playerMove", tile);
    }

    private void OnFullLobby(SocketIOEvent obj)
    {

    }
    private void OnKilledExplorer(SocketIOEvent obj)
    {

    }
}

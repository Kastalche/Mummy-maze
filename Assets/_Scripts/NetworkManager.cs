using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    //important things here, take a list of players id from surves, remove all bot logics, and state machine logics, and when server send player id and players draw.
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
        socket.On("killExplorer", OnKilledExplorer);
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

    private void OnKilledExplorer(SocketIOEvent obj)
    {
        socket.Emit("KilledExplorer");
    }
}

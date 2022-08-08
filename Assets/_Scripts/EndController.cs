using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : IStateController
{
    private GameStateManagr gameStateManagr;

    public EndController(GameStateManagr gm)
    {
        this.gameStateManagr = gm;
    }


    public void Start()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void Destroy()
    {

    }

}

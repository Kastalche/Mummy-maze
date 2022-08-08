using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : IStateController
{
    public void Start()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void Destroy()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlayerButton : MonoBehaviour
{

    public void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("MultiPlayerScene");
    }
}


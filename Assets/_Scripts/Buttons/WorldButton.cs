using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour
{
    void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("ChooseMode");
    }
}

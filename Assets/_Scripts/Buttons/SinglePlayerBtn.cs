using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerBtn : MonoBehaviour
{
    public void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

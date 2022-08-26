using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlayerButton : MonoBehaviour
{
    [SerializeField] NetworkManager mng;
    public void OnMouseUpAsButton()
    {
        mng.PlayMulti();
        SceneManager.LoadScene("MultiPlayerScene");
    }
}


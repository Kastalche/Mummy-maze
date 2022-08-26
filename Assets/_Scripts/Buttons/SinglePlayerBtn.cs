using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerBtn : MonoBehaviour
{
    [SerializeField] NetworkManager mng;
    public void OnMouseUpAsButton()
    {
        mng.PlaySingle();
        SceneManager.LoadScene("SampleScene");
    }
}

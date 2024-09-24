using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab)) SceneManager.LoadScene(2);
    }

    public void menuStart()
    {
        SceneManager.LoadScene(1);
    }
    public void menuExit()
    {
        Application.Quit();
    }
}

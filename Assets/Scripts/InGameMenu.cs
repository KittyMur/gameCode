using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    // Update is called once per frame
    public void Resume(GameObject menu)
    {
        cam.GetComponent<Looking>().nrot = true;
        player.GetComponent<Move>().esc.color = Color.white;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menu.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

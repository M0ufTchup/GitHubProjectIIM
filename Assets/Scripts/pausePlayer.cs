using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausePlayer : MonoBehaviour
{
    public GameObject menuPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            pauseON();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            pauseOFF();
        }
    }

    public void pauseON()
    {
        Time.timeScale = 0f;
        menuPause.SetActive(true);
    }

    public void pauseOFF()
    {
        Time.timeScale = 1f;
        menuPause.SetActive(false);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

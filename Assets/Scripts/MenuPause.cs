using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject menuPause;
    public GameObject menuOptions;

    public bool end = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1 && !end)
            pauseOn();
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
            pauseOff();

    }

    public void pauseOn()
    {
        Time.timeScale = 0f;
        menuPause.SetActive(true);
    }
    public void pauseOff()
    {
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        menuOptions.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void backMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void setOptions()
    {
        menuPause.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void backPause()
    {
        menuOptions.SetActive(false);
        menuPause.SetActive(true);
    }

    public void replay()
    {
        SceneManager.LoadScene("TwoSouls");
    }
}

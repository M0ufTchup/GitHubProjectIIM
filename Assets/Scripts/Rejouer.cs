using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rejouer : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Sweet Home Level");
    }
}

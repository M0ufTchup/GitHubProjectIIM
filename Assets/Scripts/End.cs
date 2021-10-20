using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public Animator hudAnimator;
    public GameObject menuPause;
    public GameObject player;

    public void Ending()
    {
        menuPause.GetComponent<MenuPause>().end = true;
        hudAnimator.SetTrigger("End");
        player.GetComponent<PlayerMovement>().stop = true;
    }
}

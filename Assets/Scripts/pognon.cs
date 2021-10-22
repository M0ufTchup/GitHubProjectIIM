using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pognon : MonoBehaviour
{
    public int coin;
    public Text textCoin;
    public GameObject damage;


    void Start()
    {
        coin = 9;
        textCoin.text = coin.ToString();
    }

    void Update()
    {

    }

    public void life()
    {
        coin--;
        textCoin.text = coin.ToString();
        if (coin == 0)
        {
            damage.GetComponent<PlayerLife>().Die();
        }
    }

    public void encounterLife()
    {
        coin++;
        textCoin.text = coin.ToString();
    }
}

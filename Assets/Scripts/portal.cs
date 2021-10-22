using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public Transform balise;
    public GameObject life;
    private pognon scriptPognon;

    void Start ()
    {
        scriptPognon = GameObject.FindGameObjectWithTag("CoinCoin").GetComponent<pognon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            scriptPognon.life();
            collision.gameObject.transform.position = balise.position;
        }
    }
}

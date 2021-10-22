using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpCollect : MonoBehaviour
{
    private int xp;
    public Text textXp;
    public Animator xpAnimator;

    void Start()
    {
        textXp.text = xp.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (xp == 0)
                xpAnimator.SetTrigger("Souls");
            xp += 10;
            textXp.text = xp.ToString();
            FindObjectOfType<Shop>().soul += 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "xp")
        {
            if (xp == 0)
                xpAnimator.SetTrigger("Souls");
            xp++;
            textXp.text = xp.ToString();
            FindObjectOfType<Shop>().soul += 1;
            Destroy(other.gameObject);
        }
    }

    public void DeadSouls()
    {
        xp = 0;
        FindObjectOfType<Shop>().soul = 0;
        textXp.text = xp.ToString();
    }

    public void shop(int price)
    {
        xp -= price;
        textXp.text = xp.ToString();
    }
}

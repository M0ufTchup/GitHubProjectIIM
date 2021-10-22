using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage;
    private int timerCoroutine = 0;

    //public HealthBar healthBar;

    public float time;
    public float decreaseTime;

    public GameObject Deathsound;

    public GameObject CanvasdelaMort;
    public float tempsDeMort;
    public GameObject life;
    private pognon scriptPognon;
    public GameObject deathTime;
    public GameObject badObject;
    public GameObject courageObject;

    public bool isCoroutineStart = false;
    public bool gogocoroutinebis = false;

    public bool courageStart = false;
    public bool couragecoroutinebis = false;

    public bool mewStart = false;
    public bool mewcoroutinebis = false;

    public static PlayerLife instance;

    void Start()
    {
        time = 60f;
        decreaseTime = 1f;
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        scriptPognon = life.GetComponent<pognon>();

        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (gogocoroutinebis && !isCoroutineStart)
            StartCoroutine(degatzone());
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(60);
        }
        /*if (couragecoroutinebis && !courageStart)
        {
            StartCoroutine(healyourself());
        }*/
    }

    public void HealPlayer(int amount)
    {
        if ((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }

        currentHealth += amount;
        //healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && scriptPognon.coin > 0)
        {
            scriptPognon.life();
            currentHealth = maxHealth;
            //healthBar.SetHealth(currentHealth);
            return;
        }
    }

    public void Die()
    {
        Debug.Log("Player is dead.");
        controlAdvenced.instance.enabled = false;
        Destroy(GetComponent<Flocon>());
        Destroy(GetComponent<Rigidbody2D>());
        controlAdvenced.instance.animatotor.SetTrigger("Die");
        Deathsound.GetComponent<AudioSource>().Play();
        CanvasdelaMort.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("actionMew"))
        {
            TakeDamage(5);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerPlatform"))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                TakeDamage(5);
            }
        }
    }

    public void gogocoroutine(bool ison)
    {
        gogocoroutinebis = ison;
    }

    private IEnumerator degatzone()
    {
        isCoroutineStart = true;

        Debug.Log("gogocoroutine");
        TakeDamage(15);
        yield return new WaitForSeconds(0.5f);

        isCoroutineStart = false;
    }

    public void couragecoroutine(bool ison)
    {
        if (couragecoroutinebis == false)
        {
            StartCoroutine(healyourself());
            Debug.Log("148");
        }
    }

    public IEnumerator healyourself()
    {
        couragecoroutinebis = true;
        Debug.Log("h");
        timerCoroutine = 0;
        while (timerCoroutine != 5)
        {
            Debug.Log("couragecoroutine");
            HealPlayer(5);
            yield return new WaitForSeconds(1f);
            timerCoroutine += 1;
        }
        couragecoroutinebis = false;
    }

    /*public void meowcoroutine(bool ison)
    {
        mewcoroutinebis = ison;
    }

    public IEnumerator imew()
    {
        mewStart = true;

        Debug.Log("mewcoroutine");
        TakeDamage(10);
        yield return new WaitForSeconds(0.5f);

        mewStart = false;
    }*/
}

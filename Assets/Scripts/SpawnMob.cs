using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMob : MonoBehaviour
{
    public GameObject enemy;
    public GameObject whereToSpawn;
    public bool spawn = false;
    public bool once;

    private int randX;
    private Vector3 additionalSpawn;

    void Start()
    {
        if (!once)
            randX = Random.Range(1,3);
        else
            randX = 0;
    }

    void Update()
    {
        if (spawn == true)
        {
            for (int i = 0; i <= randX; i++)
            {;
                if (!once)
                {
                    additionalSpawn.x = Random.Range(-1, 1);
                    additionalSpawn.y = Random.Range(-1, 1);
                }
                else
                {
                    additionalSpawn.x = 0;
                    additionalSpawn.y = 0;
                }
                Instantiate(enemy, whereToSpawn.transform.position + additionalSpawn, Quaternion.identity);
            }
            spawn = false;
            Destroy(this);
        }
    }
}

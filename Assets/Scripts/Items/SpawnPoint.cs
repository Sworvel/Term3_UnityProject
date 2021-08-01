using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] Fruit;

    public GameObject Ball, Axe, Enemy;

    bool isSpawnned = false;

    public enum item
    {
        Fruits,
        Ball,
        Axe,
        Enemy,
    }

    public item spawnnedItem;

    void Start()
    {
        switch (spawnnedItem)
        {
            case item.Fruits:
                Instantiate(Fruit[Random.Range(0, 3)], transform.position, transform.rotation);
                break;
        }
    }

    void Update()
    {
        if (!isSpawnned)
        {
            if (GameManager.instance.hasKey == true)
            {
                switch (spawnnedItem)
                {
                    case item.Ball:
                        if (Ball)
                        {
                            Instantiate(Ball, transform.position, transform.rotation);
                            isSpawnned = true;
                        }
                        break;
                }
            }

            if (GameManager.instance.hasBall == true)
            {
                switch (spawnnedItem)
                {
                    case item.Axe:
                        if (Axe)
                        {
                            Instantiate(Axe, transform.position, transform.rotation);
                            isSpawnned = true;
                        }
                        break;
                }
            }
        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            spawnEnemy();
        }
    }

    public void spawnEnemy()
    {
        switch (spawnnedItem)
        {
             case item.Enemy:
                if (Enemy)
                {
                    Instantiate(Enemy, transform.position, transform.rotation);
                }
                  break;
        }
    }
}

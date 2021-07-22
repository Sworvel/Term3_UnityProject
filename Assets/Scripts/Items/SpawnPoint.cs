using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //ItemScript itemScript;

    public GameObject[] spawnnedObject;
    //public GameObject Axe;

    //public bool axeSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //itemScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemScript>();

        //if (axeSpawn == false)
        //{
            Instantiate(spawnnedObject[Random.Range(0, 3)], transform.position, transform.rotation);
        //}
    }

    //void Update()
    //{
    //    if (axeSpawn == true)
    //    {
    //        if (itemScript.chestOpened == true)
    //        {
    //            Instantiate(Axe, transform.position, transform.rotation);
    //        }
    //    }
    //}
}

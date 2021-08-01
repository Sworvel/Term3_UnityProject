using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    ItemScript itemScript;
    SingleLinkedList sl;

    public GameObject Ball, Key, Axe;

    bool hasBall = false, hasKey = false, hasAxe = false, isempty = false;

    // Start is called before the first frame update
    void Start()
    {
        itemScript = GetComponent<ItemScript>();
        sl = GetComponent<SingleLinkedList>();

        GameObject[] items = { Ball, Key, Axe };
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void pickupItem(GameObject item)
    {
       
    }*/
}

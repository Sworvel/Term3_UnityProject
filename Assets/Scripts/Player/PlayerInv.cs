using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    ItemScript itemScript;

    LinkedList<GameObject> pickup = new LinkedList<GameObject>();

    public GameObject Ball;
    public GameObject Key;
    public GameObject Axe;

    // Start is called before the first frame update
    void Start()
    {
        itemScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pickupItem()
    {
        if (pickup.Count > 0)
        {
            LinkedListNode<GameObject> Last;
            Last = pickup.First;

            switch (Last.Value.tag)
            {
                case "Ball":

                    break;

                case "Key":

                    break;

                case "Axe":

                    break;
            }
        }
    }
}

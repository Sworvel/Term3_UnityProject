using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    MeshRenderer mr;
    Animator anim;
    PlayerInv playerInv;
    SphereCollider sc;
    SingleLinkedList sl;

    GameObject Key, Axe, Ball;

    public enum ItemType
    {
        Key,
        Chest,
        Ball,
        Axe,
        Tree,
    }

    public ItemType currentItem;

    public bool chestOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponentInParent<MeshRenderer>();
        anim = GetComponent<Animator>();
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInv>();
        sc = GetComponentInChildren<SphereCollider>();
        Key = GameObject.FindGameObjectWithTag("Key");
        Ball = GameObject.FindGameObjectWithTag("Ball");
        Axe = GameObject.FindGameObjectWithTag("Axe");
        sl = GetComponent<SingleLinkedList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (currentItem)
            {
                case ItemType.Key:
                    //playerInv.pickupItem(Key);
                    GameManager.instance.hasKey = true;
                    destroymr();
                    break;

                case ItemType.Ball:
                    GameManager.instance.hasBall = true;
                    //playerInv.pickupItem(Ball);
                    destroymr();
                    break;

                case ItemType.Chest:
                    if (other.gameObject.GetComponent<PlayerInv>())
                    {
                        if (anim.GetBool("isOpen") == false)
                        {
                            anim.SetTrigger("playerOpened");
                            Destroy(GetComponent<BoxCollider>());
                        }
                        if(anim.GetBool("isOpen") == true)
                        {
                            chestOpened = true;
                        }
                    }
                    break;

                case ItemType.Axe:
                    //playerInv.pickupItem(Axe);
                    GameManager.instance.hasAxe = true;
                    destroymr();
                    break;

                case ItemType.Tree:
                    if (other.gameObject.GetComponent<PlayerInv>())
                    {

                    }
                    break;
            }
        }
    }

    public void destroymr()
    {
        Destroy(mr);
    }

    void chestIsOpen()
    {
        anim.SetBool("isOpen", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    MeshRenderer mr;
    Animator anim;
    PlayerInv playerInv;

    public enum ItemType
    {
        Key,
        Chest,
        Axe,
        Tree,
    }

    public ItemType currentItem;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponentInParent<MeshRenderer>();
        anim = GetComponent<Animator>();
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInv>();
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
                    playerInv.hasKey = true;
                    destroymr();
                    break;

                case ItemType.Chest:
                    if (other.gameObject.GetComponent<PlayerInv>())
                    {
                        if (anim.GetBool("isOpen") == false && other.gameObject.GetComponent<PlayerInv>().hasKey == true)
                        {
                            anim.SetTrigger("playerOpened");
                        }
                        if(anim.GetBool("isOpen") == true)
                        {
                            
                        }
                    }
                    break;

                case ItemType.Axe:
                    playerInv.hasKey = false;
                    playerInv.hasAxe = true;
                    destroymr();
                    break;

                case ItemType.Tree:
                    if (other.gameObject.GetComponent<PlayerInv>())
                    {
                        if (other.gameObject.GetComponent<PlayerInv>().hasAxe == true)
                        {

                        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeele : MonoBehaviour
{
    public bool inAttackRange;
    BoxCollider attackBox;

    private void Start()
    {
        attackBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            inAttackRange = true;
        }
        else
            inAttackRange = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            inAttackRange = false;
        }
    }
}

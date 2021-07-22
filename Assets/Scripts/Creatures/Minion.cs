using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    MeshRenderer mr;

    float angle = 15;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponentInParent<NavMeshAgent>();
        mr = GetComponentInParent<MeshRenderer>();
    }

    void Update()
    {
        if (player != null && Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle)
        {
            mr.enabled = false;
            enemy.isStopped = true;
            enemy.destination = player.transform.position;
        }
        else
        {
            mr.enabled = true;
            enemy.isStopped = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.PlayerDeath();
        }
    }

}

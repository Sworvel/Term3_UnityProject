using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    GameObject player;
    NavMeshAgent enemy;
    SpawnPoint sp;
    Animator anim;

    float angle = 15;

    void Update()
    {
        if(!player)
        player = GameObject.FindGameObjectWithTag("Player");

        if(!enemy)
        enemy = GetComponent<NavMeshAgent>();

        if(!sp)
        sp = GetComponent<SpawnPoint>();

        if(!anim)
        anim = GetComponent<Animator>();

        if (player != null && Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle)
        {
            enemy.isStopped = true;
            anim.SetBool("isWalking", false);
        }
        else
        {
            enemy.isStopped = false;
            anim.SetBool("isWalking", true);
        }
        enemy.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking", true);
            GameManager.instance.playerHealth--;
            GameManager.instance.enemyFinishedDeath();
        }

        if(collision.gameObject.tag == "PlayerProjectile")
        {
            anim.SetBool("isDead", true);
            GameManager.instance.enemyFinishedDeath();
            sp.spawnEnemy();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking", false);
        }
    }

}

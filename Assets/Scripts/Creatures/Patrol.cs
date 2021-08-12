using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] points;
    private GameObject[] findPoints; 
    public float agroRange;
    //private int pointLength = 0;
    private int destPoint = 0;
    private NavMeshAgent enemy;
    private GameObject player;
    private Animator anim;
    private bool enemyIsDead;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        enemyIsDead = false;

        /*if (pointLength > 0)
            pointLength = 0;*/

        if (agroRange <= 0)
        {
            agroRange = 10;
            Debug.Log("Agro Range 0, set to 10");
        }

        /*foreach (Transform point in points)
        {
            pointLength++;
        }*/

        // Disabling auto-braking allows for continuous movement
        enemy.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            anim.SetBool("isWalking", false);
        }

        enemy.destination = points[destPoint].position;

        // Choose the next point in the array as the destination
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        if (enemyIsDead == false)
        {
            if (!enemy.pathPending && enemy.remainingDistance < 0.5f)
                GotoNextPoint();

            if (enemy.speed > 0)
                anim.SetBool("isWalking", true);
            else
                anim.SetBool("isWalking", false);

            if (player)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance <= agroRange)
                {
                    enemy.destination = player.transform.position;
                }
                else
                {
                    enemy.destination = points[destPoint].position;
                }
            }
        }
        else if(enemyIsDead == true)
        {
            enemy.Stop();
            anim.SetBool("isWalking", false);
            anim.SetBool("isDead", true);
            GameManager.instance.enemyFinishedDeath();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking", true);
            GameManager.instance.playerHealth--;
        }

        if (collision.gameObject.tag == "PlayerProjectile")
        {
            enemyIsDead = true;
        }
    }

    /*private void _enemyDeath()
    {
        anim.SetBool("isDead", true);
        GameManager.instance.enemyFinishedDeath();
    }*/

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking", false);
        }
    }
}

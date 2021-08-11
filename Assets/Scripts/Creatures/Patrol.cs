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

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        /*if (pointLength > 0)
            pointLength = 0;*/

        if(agroRange <= 0)
        {
            agroRange = 10;
            Debug.Log("Agro Range 0, set to 10");
        }

        /*foreach (Transform point in points)
        {
            pointLength++;
        }*/

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the enemy doesn't slow down as it
        // approaches a destination point).
        enemy.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
        {
           
        }

        // Set the enemy to go to the currently selected destination.
        enemy.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the enemy gets
        // close to the current one.
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
                //enemy.isStopped = false;
                //anim.SetBool("isWalking", true);
                enemy.destination = player.transform.position;
            }
            else
            {
                //enemy.isStopped = true;
                //anim.SetBool("isWalking", false);
                enemy.destination = points[destPoint].position;
            }
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
            anim.SetBool("isDead", true);
            GameManager.instance.enemyFinishedDeath();
            //sp.spawnEnemy();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    PlayerMeele playerMeele;
    Animator anim;

    [Header("Player Settings")]
    [Space(2)]
    [Tooltip("Speed Between 1 and 12")]
    [Range(1.0f, 12.0f)]
    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;

    Vector3 moveDirection;

    enum ControllerType { SimpleMove, Move };
    [SerializeField] ControllerType type;

    [Header("Weapon Settings")]
    // Handle weapon shooting
    public float projectileForce;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    [Header("Raycast Settings")]
    public Transform thingToLookFrom;
    public float lookAtDistance;

    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            playerMeele = GetComponentInChildren<PlayerMeele>();

            controller.minMoveDistance = 0.0f;

            if(speed <= 0)
            {
                speed = 10.0f;
                Debug.Log("Speed not set on speed, defaulting to 10.");
            }

            if (jumpSpeed <= 0)
            {
                jumpSpeed = 6.0f;
                Debug.Log("Jump Speed not set on jump speed, defaulting to 6.");
            }

            if (rotationSpeed <= 0)
            {
                rotationSpeed = 10.0f;
                Debug.Log("Rotating Speed not set on rotation speed, defaulting to 10.");
            }

            if (gravity <= 0)
            {
                gravity = 9.81f;
                Debug.Log("Gravity not set on gravity, defaulting to 9.81.");
            }

            moveDirection = Vector3.zero;

            if (projectileForce <= 0)
            {
                projectileForce = 10.0f;
                Debug.Log("ProjectileForce not set on projectilePrefab force defaulting to " + projectileForce);
            }

            if (!projectilePrefab)
                Debug.LogWarning("Missing projectilePrefab on " + name);

            if (!projectileSpawnPoint)
                Debug.LogWarning("Missing projectileSpawnPoint on " + name);
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        finally
        {
            Debug.LogWarning("Always get called");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerHealth > 0)
        {
            switch (type)
            {
                case ControllerType.SimpleMove:

                    controller.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

                    break;

                case ControllerType.Move:

                    if (controller.isGrounded)
                    {
                        anim.SetBool("isGrounded", true);
                        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                        moveDirection *= speed;

                        moveDirection = transform.TransformDirection(moveDirection);

                        if (Input.GetButtonDown("Jump"))
                        {
                            moveDirection.y = jumpSpeed;
                            anim.SetBool("isJumping", true);
                        }
                    }

                    if (!controller.isGrounded)
                        anim.SetBool("isGrounded", false);

                    if (controller.velocity.x != 0 || controller.velocity.z != 0)
                    {
                        anim.SetBool("isMoving", true);
                    }
                    else
                    {
                            anim.SetBool("isMoving", false);
                    }

                    moveDirection.y -= gravity * Time.deltaTime;

                    controller.Move(moveDirection * Time.deltaTime);

                    break;
            }
        }
        else if (GameManager.instance.playerHealth <= 0)
            GameManager.instance.PlayerDeath();

        if (Input.GetButtonDown("Fire1"))
        {
            if (projectileSpawnPoint && projectilePrefab)
            {
                GameObject temp = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);

                temp.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, projectileForce));
                anim.SetBool("isShooting", true);

                Destroy(temp.gameObject, 2.0f);
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("isPunching", true);
            if (playerMeele.inAttackRange == true)
            {
                GameManager.instance.enemyFinishedDeath();
            }
        }
        else
            anim.SetBool("isPunching", false);
    }

    public void finishedDeath()
    {
        GameManager.instance.PlayerFinishedDeath();
    }

    [ContextMenu("Reset Stats")]
    void ResetStats()
    {
        speed = 6.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

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
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;
    public GameObject fire;

    [Header("Raycast Settings")]
    public Transform thingToLookFrom;
    public float lookAtDistance;

    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();

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
                Debug.Log("ProjectileForce not set on projectile force defaulting to " + projectileForce);
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
        switch (type)
        {
            case ControllerType.SimpleMove:

                controller.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

                break;

            case ControllerType.Move:

                if(controller.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    
                    moveDirection *= speed;
                    
                    moveDirection = transform.TransformDirection(moveDirection);

                    if (Input.GetButtonDown("Jump"))
                        moveDirection.y = jumpSpeed;
                }

                moveDirection.y -= gravity * Time.deltaTime;

                controller.Move(moveDirection * Time.deltaTime);

                break;
        }

        RaycastHit hit;

        if (!thingToLookFrom)
        {
            Debug.DrawRay(transform.position, transform.forward * lookAtDistance, Color.red);

            if (Physics.Raycast(transform.position, transform.forward, out hit, lookAtDistance))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
            }
        }
        else
        {
            Debug.DrawRay(thingToLookFrom.transform.position, thingToLookFrom.transform.forward * lookAtDistance, Color.blue);

            if (Physics.Raycast(thingToLookFrom.transform.position, thingToLookFrom.transform.forward, out hit, lookAtDistance))
            {
                if(hit.transform.name == "Minion" && Input.GetButtonDown("Fire1"))
                {
                    GameManager.instance.MinionDeath();
                }
            }
        }


        /*if (Input.GetButtonDown("Fire1"))
        {
            playerFire();
        }*/
    }

    /*public void playerFire()
    {
        if (projectileSpawnPoint && projectilePrefab)
        {
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position,
                projectileSpawnPoint.rotation);

            temp.AddForce(projectileSpawnPoint.forward * projectileForce, ForceMode.Force);

            Destroy(temp.gameObject, 2.0f);
        }
    }*/

    [ContextMenu("Reset Stats")]
    void ResetStats()
    {
        speed = 6.0f;
    }
}

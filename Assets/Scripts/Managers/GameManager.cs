using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float fixedDeltaTime = 1.0f;

    static GameManager _instance = null;

    private GameObject enemy;
    private Animator playerAnim;
    public GameObject player;
    CanvasManager cm;
    public Transform playerSpawn;

    public bool hasKey = false, hasBall = false, hasAxe = false;

    public int playerHealth;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerHealth < 0)
        {
            playerHealth = 3;
            Debug.Log("health not set, default to 3");
        }

        this.fixedDeltaTime = Time.fixedDeltaTime;

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        cm = GameObject.FindGameObjectWithTag("EndSpace").GetComponent<CanvasManager>();

        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

        if(SceneManager.GetActiveScene().name == "SceneOne")
        {
            /*if (!player)
            {
                if (playerSpawn)
                {
                    SpawnPlayer(playerSpawn);
                    _setPlayerAnim();
                }
            }*/
            if (Input.GetButtonDown("Cancel"))
            {
                cm.ShowPauseMenu();
            }
        }
    }

    //Main Manager Functions
    public void StartLevelOne()
    {
        SceneManager.LoadScene("SceneOne");
    }

    public void StartLevelTwo()
    {
        SceneManager.LoadScene("SceneTwo");
    }

    public void PlayerDeath()
    {
        playerAnim.SetBool("isDead", true);
    }

    public void PlayerFinishedDeath()
    {
        SceneManager.LoadScene("DeathScene");
    }

   /* public void SpawnPlayer(Transform spawnlocation)
    {
        Instantiate(player, spawnlocation.position, spawnlocation.rotation);
    }*/

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void enemyFinishedDeath()
    {
        Destroy(enemy, 3f);
    }

    /*void _setPlayerAnim()
    {
        if(player)
            playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }*/
}

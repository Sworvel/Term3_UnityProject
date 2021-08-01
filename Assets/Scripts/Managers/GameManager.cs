using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float fixedDeltaTime = 1.0f;

    static GameManager _instance = null;

    GameObject enemy;
    Animator playerAnim;

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
}

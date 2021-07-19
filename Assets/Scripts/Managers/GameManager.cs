using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float fixedDeltaTime = 1.0f;

    static GameManager _instance = null;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;

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

    //Item Funtions
    public void hasKey()
    {

    }
}

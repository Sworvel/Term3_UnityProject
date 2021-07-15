using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button QuitButton;
    public Button ResumeButton;

    [Header("Menus")]
    public GameObject PauseMenu;
    public GameObject DeathMenu;

    [Header("EndText")]
    public Transform endText;

    // Start is called before the first frame update
    void Start()
    {
        if (QuitButton)
        {
            QuitButton.onClick.AddListener(() => QuitGame());
        }
        if (ResumeButton)
        {
            ResumeButton.onClick.AddListener(() => ReturnToGame());
        }
        PauseMenu.SetActive(false);
        DeathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        endText.transform.LookAt(Camera.main.transform);

        if (Input.GetButtonDown("Cancel"))
        {
            ShowPauseMenu();
        }
    }

    //Show Menu on Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowPauseMenu();
        }
    }

    void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void ReturnToGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowDeathMenu()
    {
        DeathMenu.SetActive(true);
        //Time.timeScale = 0.5f;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

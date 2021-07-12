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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowPauseMenu();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ReturnToGame();
    }

    void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
    }

    void ReturnToGame()
    {
        PauseMenu.SetActive(false);
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

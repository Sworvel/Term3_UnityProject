using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button QuitButton;
    public Button QuitButton2;
    public Button ResumeButton;
    public Button lvlOneButton;
    public Button lvlTwoButton;

    [Header("Menus")]
    public GameObject PauseMenu;
    public GameObject lvlSelect;

    [Header("EndText")]
    public Transform endText;

    // Start is called before the first frame update
    void Start()
    {
        if (QuitButton)
        {
            QuitButton.onClick.AddListener(() => GameManager.instance.QuitGame());
        }
        if (ResumeButton)
        {
            ResumeButton.onClick.AddListener(() => ReturnToGame());
        }
        if (lvlOneButton)
        {
            lvlOneButton.onClick.AddListener(() => GameManager.instance.StartLevelOne());
        }
        if (lvlTwoButton)
        {
            lvlTwoButton.onClick.AddListener(() => GameManager.instance.StartLevelTwo());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main)
        {
            if (endText)
            {
                endText.transform.LookAt(Camera.main.transform);
            }
        }
        if (QuitButton2)
        {
            QuitButton2.onClick.AddListener(() => GameManager.instance.QuitGame());
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

    public void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void ReturnToGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}

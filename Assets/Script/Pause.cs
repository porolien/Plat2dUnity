using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Canvas canvasOptions;
    public Button buttonResume;
    public Button buttonOption;
    public Button buttonQuitter;

    void Start()
    {
        buttonResume.onClick.AddListener(isResume);
        buttonOption.onClick.AddListener(Options);
        buttonQuitter.onClick.AddListener(LoadMenu);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                isResume();
            }
            else
            {
                isPause();
            }
        }

    }

    void isResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Options()
    {
        canvasOptions.gameObject.SetActive(true);
    }
    void isPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
       // SceneManager.LoadScene("mainMenu");
    }
}

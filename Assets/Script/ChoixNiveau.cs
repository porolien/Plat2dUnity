using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoixNiveau : MonoBehaviour
{
    public string levelToload;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToload);
    }
}

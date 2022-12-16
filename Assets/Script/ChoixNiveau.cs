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
    private void OnTriggerEnter2D(Collider2D triggered)
    {
        if (triggered.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(levelToload);
        }
    }
}

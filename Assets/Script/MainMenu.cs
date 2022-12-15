using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*public string levelToload;*/

    public GameObject settingsWindow;

    public GameObject créditsWindow;

    public GameObject ChoseLevelWindow;

    public GameObject SonsWindow;

    public GameObject CommandeWindow;

    public GameObject GraphiqueWindow;

    bool fghj;


    /*public void StartGame()
    {
        SceneManager.LoadScene(levelToload);
    }*/

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void crédits()
    {
        créditsWindow.SetActive(true);
    }

    public void CloseCréditsWindow()
    {
        créditsWindow.SetActive(false);
    }

    public void ChoixNiveaus()
    {
        ChoseLevelWindow.SetActive(true);
    }

    public void CloseChoixNiveausWindow()
    {
        ChoseLevelWindow.SetActive(false);
    }

    public void sons()
    {
        SonsWindow.SetActive(true);
        
    }

    public void CloseSonsWindow()
    {
        SonsWindow.SetActive(false);
    }

    public void Commande()
    {
        CommandeWindow.SetActive(true);
    }

    public void CloseCommandeWindow()
    {
        CommandeWindow.SetActive(false);
    }

    public void Graphique()
    {
        GraphiqueWindow.SetActive(true);
    }

    public void CloseGraphiqueWindow()
    {
        GraphiqueWindow.SetActive(false);
    }
}

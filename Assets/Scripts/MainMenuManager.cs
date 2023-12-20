using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject startScreen;

    public void LoadCharacterSelection()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSettings()
    {
        startScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }
 

    public void QuitGame()
    {
        Application.Quit();
    }
}

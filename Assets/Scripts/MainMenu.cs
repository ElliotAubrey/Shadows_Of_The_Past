using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    public void Play()
    {
        SceneManager.LoadScene("Intro");
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OptionsQuit()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

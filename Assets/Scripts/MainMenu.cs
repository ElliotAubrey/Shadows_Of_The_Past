using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Texture2D cursor;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle doorTransitions;
    [SerializeField] Toggle fullScreen;

    FMOD.Studio.VCA masterVCA;

    private void Awake()
    {
        Cursor.SetCursor(cursor, new Vector3(10.5f, 10.5f, 0), CursorMode.Auto);
        masterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Master VCA");
        Debug.Log(masterVCA);
        if (PlayerPrefs.GetInt("FirstTimeLoad") == 0)
        {
            PlayerPrefs.SetFloat("MasterVolume", 1);
            PlayerPrefs.SetInt("DoorTransitions", 1);
        }
        masterVCA.setVolume(PlayerPrefs.GetFloat("MasterVolume"));
        PlayerPrefs.SetInt("FirstTimeLoad", 1);
    }

    public void Play()
    {
        SceneManager.LoadScene("Intro");
        SceneManager.UnloadSceneAsync("Menu");
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        doorTransitions.isOn = PlayerPrefs.GetInt("DoorTransitions") == 1 ? true : false;
    }

    public void OptionsQuit()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void SoundSliderChange(float value)
    {
        masterVCA.setVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
        Debug.Log(value);
    }

    public void SetDoorTransitions(bool onOff)
    {
        PlayerPrefs.SetInt("DoorTransitions", onOff == true ? 1 : 0);
        Debug.Log(onOff);
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    PlayerGun gun;

    private void Awake()
    {
        gun = FindObjectOfType<PlayerGun>();
    }

    bool paused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            paused = true;
            gun.canFire = false;
        }
        else if (paused && Input.GetKeyDown(KeyCode.Escape))
        {
            UnPause();
        }
    }

    public void UnPause()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine("EnableGun");
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator EnableGun()
    {
        yield return new WaitForSeconds(0.5f);
        gun.canFire = true;
        Debug.Log(gun.canFire);
    }
}

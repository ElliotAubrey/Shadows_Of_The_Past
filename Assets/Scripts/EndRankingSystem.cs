using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class EndRankingSystem : MonoBehaviour
{
    [SerializeField] GameObject endScreen;
    [SerializeField] TextMeshProUGUI notesText, enemiesText, keysText, timeText, completionPercent;

    Note[] notes;
    HealthSystem[] enemies;
    KeyCollection[] keys;
    PlayerInventory inventory;
    PlayerController controller;
    PlayerGun gun;
    int noteNum, enemiesNum, keyNum;
    float elapsedTime;
    bool timerOn = true;

    private void Awake()
    {
        notes = FindObjectsOfType<Note>();
        enemies = FindObjectsOfType<HealthSystem>();
        inventory = FindObjectOfType<PlayerInventory>();
        keys = FindObjectsOfType<KeyCollection>();
        controller = FindObjectOfType<PlayerController>();
        gun = FindObjectOfType<PlayerGun>();
    }

    private void FixedUpdate()
    {
        if (timerOn) elapsedTime += Time.deltaTime;
    }
    public void EnemyKilled()
    {
        enemiesNum++;
    }

    [ContextMenu("Calc Result")]
    public void CalcResult()
    {
        timerOn = false;
        TimeSpan timePlaying = TimeSpan.FromSeconds(elapsedTime);
        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i].GetRead()) noteNum++;
        }
        notesText.text = "Notes Collected: " + noteNum.ToString() + " / " + notes.Length.ToString();
        int e = enemies.Length - 1;
        enemiesText.text = "Enemies Killed: " + enemiesNum.ToString() + " / " + e.ToString();
        keysText.text = "Keys Collected: " + inventory.keys.Count + " / " + keys.Length.ToString();
        timeText.text = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        endScreen.SetActive(true);
        Time.timeScale = 0;
        controller.canMove = false;
        gun.canFire = false;
        FindObjectOfType<InsanitySound>().Stop();
        keyNum = inventory.keys.Count;
        float x = noteNum + enemiesNum + keyNum;
        Debug.Log(x);
        float y = notes.Length + enemies.Length - 1 + keys.Length;
        Debug.Log(y);
        float z = (x / y) * 100;
        int result = (int)z;
        completionPercent.text = result.ToString("") + "% Completion";
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CalcResult();
            FindObjectOfType<MusicBehaviour>().StartMusic();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class RespawnSystem : MonoBehaviour
{
    public Light2D light;
    public Transform spawnPoint;
    public bool spawnSet = false;
    public Sprite lit, unlit;


    SpriteRenderer sr;
    RespawnSystem[] allSystems;
    TextMeshProUGUI prompt;
    HealthSystem playerHealth;

    bool inTrigger = false;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        allSystems = FindObjectsOfType<RespawnSystem>();
        prompt = GameObject.FindGameObjectWithTag("Prompt").GetComponent<TextMeshProUGUI>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.F)) SetSpawn();
    }

    private void SetSpawn()
    {
        for (int i = 0; i < allSystems.Length; i++)
        {
            allSystems[i].spawnSet = false;
            allSystems[i].sr.sprite = unlit;
            allSystems[i].light.enabled = false;
        }
        spawnSet = true;
        sr.sprite = lit;
        light.enabled = true;
        prompt.text = "Spawn set here";
        playerHealth.mySpawnPoint = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!spawnSet) prompt.text = "Press F to set spawn point";
            else prompt.text = "Spawn set here";
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.text = string.Empty;
            inTrigger = false;
        }
    }
}

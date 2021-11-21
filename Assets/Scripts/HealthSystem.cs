using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using FMODUnity;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] bool player;
    [SerializeField] Slider healthSlider;
    [SerializeField] StudioEventEmitter hit;

    public RespawnSystem mySpawnPoint;
    public float health;

    PlayerPerkManager ppManager;
    EndRankingSystem ers;

    private void Awake()
    {
        if (!player)
        {
            health = (75 + UnityEngine.Random.Range(-20, 20)) * 4;
            maxHealth = health;
            ers = FindObjectOfType<EndRankingSystem>();
        }
        if (player)
        {
            ppManager = FindObjectOfType<PlayerPerkManager>();
            health = maxHealth;
            healthSlider.value = health;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
        if (health > maxHealth) health = maxHealth;
        if(player) healthSlider.value = health;
        else if(hit)
        {
            if (hit.enabled) hit.enabled = false;
            hit.enabled = true;
        }
    }

    private void Die()
    {
        health = 0;
        if (player)
        {
            healthSlider.value = health;
            if (!ppManager) ppManager = GetComponent<PlayerPerkManager>();
            ppManager.Death();
            transform.position = mySpawnPoint.spawnPoint.position;
            health = maxHealth;
        }
        else
        {
            Destroy(gameObject,1);
            GetComponentInChildren<ParticleSystem>().Play();
            GetComponent<EnemyAI>().enabled = false;
            GetComponent<AIDestinationSetter>().target = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            ers.EnemyKilled();
        }
    }

    public bool GetPlayer()
    {
        return player;
    }

    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
        if (health > maxHealth) health = maxHealth;
        healthSlider.value = health;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using FMODUnity;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float sightRange = 5f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] BoxCollider2D attackHitbox;
    [SerializeField] AIDestinationSetter destinationSetter;
    [SerializeField] LayerMask losMask;
    [SerializeField] GameObject pointOfIntrest;
    [SerializeField] Animator controller;
    [SerializeField] StudioEventEmitter screams;

    Transform player;
    float nextAttackTime = 0f;
    bool attacking = false;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pointOfIntrest.transform.parent = null;
        StartCoroutine("Screams");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < sightRange)
        {
            if (Physics2D.Linecast(transform.position, player.position, losMask).collider.tag == "Player")
            {
                destinationSetter.target = player;
                pointOfIntrest.transform.position = player.position;
                //if(Time.time >= nextAttackTime) Attack();
                controller.SetBool("Moving", true);
            }
            else controller.SetBool("Moving", false);
        }
        else
        {
            destinationSetter.target = pointOfIntrest.transform.position != Vector3.zero ? pointOfIntrest.transform : transform;
            if (Vector3.Distance(transform.position, pointOfIntrest.transform.position) <= 1)
            {
                pointOfIntrest.transform.position = Vector3.zero;
                controller.SetBool("Moving", false);
            }
        }
        if (distance <= attackRange && !attacking) StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        Debug.Log("Attack");
        controller.SetBool("Attacking", true);
        attacking = true;
        attackHitbox.enabled = true;
        nextAttackTime = Time.time + timeBetweenAttacks;
        yield return new WaitForSeconds(timeBetweenAttacks/2);
        if(attackHitbox.enabled) attackHitbox.enabled = false;
        attacking = false;
        controller.SetBool("Attacking", false);
    }

    IEnumerator Screams()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 10));
            if (screams.enabled) screams.enabled = false;
            screams.enabled = true;
        }
    }
}

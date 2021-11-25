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
    [SerializeField] Transform startPos;
    [SerializeField] Animator controller;
    [SerializeField] StudioEventEmitter screams, attack;

    Transform player;
    float nextAttackTime = 0f;
    bool attacking = false;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pointOfIntrest.transform.parent = null;
        startPos.transform.parent = null;
        StartCoroutine("Screams");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < sightRange)
        {
            if (Physics2D.Linecast(transform.position, player.position, losMask).collider.tag == "Player" && !attacking)
            {
                destinationSetter.target = player;
                pointOfIntrest.transform.position = player.position;
                //if(Time.time >= nextAttackTime) Attack();
                controller.SetBool("Moving", true);
            }

            else
            {
                controller.SetBool("Moving", false);
            }
        }
        else
        {
            destinationSetter.target = pointOfIntrest.transform.position != Vector3.zero ? pointOfIntrest.transform : startPos;
            if (Vector3.Distance(transform.position, pointOfIntrest.transform.position) <= 2)
            {
                pointOfIntrest.transform.position = Vector3.zero;
                controller.SetBool("Moving", false);
            }
        }
        if (distance <= attackRange && !attacking) StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        if (attack.enabled) attack.enabled = false;
        attack.enabled = true;
        controller.SetBool("Attacking", true);
        controller.SetBool("Moving", false);
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

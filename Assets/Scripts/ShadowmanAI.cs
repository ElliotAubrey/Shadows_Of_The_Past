using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Pathfinding;
using FMODUnity;

public class ShadowmanAI : MonoBehaviour
{
    [SerializeField] List<ShadowCaster2D> casters;
    [SerializeField] float timeBetweenChanges = 0.1f;
    [SerializeField] float sightRange = 5f;
    [SerializeField] AIDestinationSetter destinationSetter;
    [SerializeField] LayerMask losMask;
    [SerializeField] GameObject pointOfIntrest;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] ParticleSystem particles;
    [SerializeField] StudioEventEmitter screams;

    bool shadowsControl = true;
    Transform player;
    float health;
    bool loseHealth = false;

    private void Awake()
    {
        int shadow = Random.Range(0, casters.Count);
        for (int i = 0; i < casters.Count; i++)
        {
            casters[i].enabled = false;
            if (i == shadow) casters[i].enabled = true;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pointOfIntrest.transform.parent = null;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (shadowsControl) StartCoroutine("RandomShadows");

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < sightRange)
        {
            if (Physics2D.Linecast(transform.position, player.position, losMask).collider.tag == "Player")
            {
                destinationSetter.target = player;
                pointOfIntrest.transform.position = player.position;
            }
        }
        else
        {
            destinationSetter.target = pointOfIntrest.transform.position != Vector3.zero ? pointOfIntrest.transform : transform;
            if (Vector3.Distance(transform.position, pointOfIntrest.transform.position) <= 1) pointOfIntrest.transform.position = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if(loseHealth)
        {
            particles.Play();
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("ShadowDead");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("LanternColliders"))
        {
            loseHealth = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LanternColliders"))
        {
            loseHealth = false;
        }
    }

    IEnumerator RandomShadows()
    {
        shadowsControl = false;
        yield return new WaitForSeconds(timeBetweenChanges);
        int shadow = Random.Range(0, casters.Count);
        for (int i = 0; i < casters.Count; i++)
        {
            casters[i].enabled = false;
            if (i == shadow) casters[i].enabled = true;
        }
        shadowsControl = true;
    }
}

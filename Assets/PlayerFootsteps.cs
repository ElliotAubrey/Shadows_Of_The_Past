using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public enum GroundType { Carpet, Wood, Metal }
public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] StudioEventEmitter carpet, wood, metal;
    PlayerController controller;
    GroundType gType = GroundType.Carpet;

    private void Awake()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(controller.isMoving)
        {
            if (gType == GroundType.Carpet) carpet.enabled = true;
            if (gType == GroundType.Wood) wood.enabled = true;
            if (gType == GroundType.Metal) metal.enabled = true;
        }
        else
        {
            Disable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Carpet")
        {
            Disable();
            gType = GroundType.Carpet;
        }
        if (collision.tag == "Wood")
        {
            Disable();
            gType = GroundType.Wood;
        }
        if (collision.tag == "Metal")
        {
            Disable();
            gType = GroundType.Metal;
        }
    }


    private void Disable()
    {
        carpet.enabled = false;
        wood.enabled = false;
        metal.enabled = false;
    }
}

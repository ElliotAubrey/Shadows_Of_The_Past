using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShadowCasterEnabler : MonoBehaviour
{
    [SerializeField] bool enable = false;

    ShadowCaster2D caster;
    private void Awake()
    {
        caster = GameObject.FindGameObjectWithTag("Player").GetComponent<ShadowCaster2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            caster.enabled = enable;
        }
    }

    private void OnDisable()
    {
        caster.enabled = false;
    }

}

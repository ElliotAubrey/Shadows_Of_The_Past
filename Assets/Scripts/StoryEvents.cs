using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class StoryEvents : MonoBehaviour
{
    [SerializeField] GameObject[] thingsToDisable;
    [SerializeField] GameObject[] thingsToEnable;
    [SerializeField] StudioEventEmitter boilerSound;

    bool inTrigger = false;
    bool occured = false;
    bool disabled = false;
    private void Update()
    {
        if (inTrigger && !occured)
        {
            StartCoroutine("BoilerExplosion");
            boilerSound.enabled = true;
            occured = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

    IEnumerator BoilerExplosion()
    {
        Debug.Log("Test");
        yield return new WaitForSeconds(3.5f);
        for (int i = 0; i < thingsToDisable.Length; i++)
        {
            thingsToDisable[i].SetActive(false);
        }
        for (int i = 0; i < thingsToEnable.Length; i++)
        {
            thingsToEnable[i].SetActive(true);
        }

    }

}

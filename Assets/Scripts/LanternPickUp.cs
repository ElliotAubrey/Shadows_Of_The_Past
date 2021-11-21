using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class LanternPickUp : MonoBehaviour
{
    [SerializeField] StudioEventEmitter pickUp;
    PlayerLantern lantern;
    TextMeshProUGUI pickUpText;
    SpriteRenderer sr;
    BoxCollider2D bc;

    private void Awake()
    {
        lantern = FindObjectOfType<PlayerLantern>();
        pickUpText = GameObject.FindGameObjectWithTag("PickUpText").GetComponent<TextMeshProUGUI>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickUp.enabled = true;
            lantern.collected = true;
            sr.enabled = false;
            bc.enabled = false;
            StartCoroutine("PickUp");
        }
    }

    IEnumerator PickUp()
    {
        pickUpText.text = "Lantern acquired";
        yield return new WaitForSeconds(2.5f);
        pickUpText.text = string.Empty;
        Destroy(gameObject);
    }
}

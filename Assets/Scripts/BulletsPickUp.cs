using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class BulletsPickUp : MonoBehaviour
{
    [SerializeField] PlayerGun gun;
    [SerializeField] StudioEventEmitter pickUp;

    public int amount;

    TextMeshProUGUI pickUpText;
    SpriteRenderer sr;
    BoxCollider2D bc;

    private void Awake()
    {
        pickUpText = GameObject.FindGameObjectWithTag("PickUpText").GetComponent<TextMeshProUGUI>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gun.PickUpAmmo(amount);
            StartCoroutine("PickUp");
            sr.enabled = false;
            bc.enabled = false;
            pickUp.enabled = true;
        }
    }

    IEnumerator PickUp()
    {
        pickUpText.text = "+" + amount + " bullets";
        yield return new WaitForSeconds(2.5f);
        pickUpText.text = string.Empty;
        Destroy(gameObject);
    }
}

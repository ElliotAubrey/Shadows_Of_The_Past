using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanternFuelPickUp : MonoBehaviour
{
    [SerializeField] PlayerLantern lantern;
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
        if(collision.CompareTag("Player"))
        {
            lantern.AddFuel(1);
            StartCoroutine("PickUp");
            sr.enabled = false;
            bc.enabled = false;
        }
    }

    IEnumerator PickUp()
    {
        pickUpText.text = "+ 1 fuel";
        yield return new WaitForSeconds(2.5f);
        pickUpText.text = string.Empty;
        Destroy(gameObject);
    }
}

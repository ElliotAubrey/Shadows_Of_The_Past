using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;using FMODUnity;
public class KeyCollection : MonoBehaviour
{
    [SerializeField] Key key;
    [SerializeField] StudioEventEmitter pickUp;

    TextMeshProUGUI pickUpText;
    PlayerInventory inventory;
    SpriteRenderer sr;
    BoxCollider2D bc;

    private void Awake()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        pickUpText = GameObject.FindGameObjectWithTag("PickUpText").GetComponent<TextMeshProUGUI>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.AddKey(key);
            StartCoroutine("PickUp");
            sr.enabled = false;
            bc.enabled = false;
            pickUp.enabled = true;
        }
    }

    IEnumerator PickUp()
    {
        pickUpText.text = key.keyName + " acquired";
        yield return new WaitForSeconds(2.5f);
        pickUpText.text = string.Empty;
        Destroy(gameObject);
    }
}

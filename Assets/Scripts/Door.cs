using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] string doorName;
    [SerializeField] Transform exitPoint;
    [SerializeField] StudioEventEmitter open, unlock;
    [SerializeField] GameObject transition;

    public bool locked = false;
    public bool hasKey;
    public Key requiredKey;


    GameObject player;
    TextMeshProUGUI prompt;
    bool inTrigger;
    bool transitions;

    private void Awake()
    {
        prompt = GameObject.FindGameObjectWithTag("Prompt").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        transitions = PlayerPrefs.GetInt("DoorTransitions") == 1 ? true : false;
    }


    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (!locked)
            {
                if (open.enabled) open.enabled = false;
                open.enabled = true;
                if (transitions) StartCoroutine("DoorTransition");
                else player.transform.position = exitPoint.position;

            }
            else if (!player.GetComponent<PlayerInventory>().CheckKey(requiredKey))
            {
                prompt.text = "This door is locked - Requires : " + requiredKey.keyName;
            }
            else
            {
                locked = false;
                prompt.text = "Unlocked " + doorName;
                unlock.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.text = "Press F to enter " + doorName;
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.text = string.Empty;
            inTrigger = false;
        }
    }

    IEnumerator DoorTransition()
    {
        player.transform.position = new Vector3(100, 100, 0);
        transition.SetActive(true);
        this.enabled = false;
        yield return new WaitForSeconds(1);
        transition.SetActive(false);
        this.enabled = true;
        player.transform.position = exitPoint.position;
    }
}


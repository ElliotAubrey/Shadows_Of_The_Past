using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class StoryEvents : MonoBehaviour
{
    [SerializeField] string[] messages;
    [SerializeField] GameObject[] thingsToDisable;
    [SerializeField] GameObject[] thingsToEnable;
    [SerializeField] StudioEventEmitter boilerSound;


    TextMeshProUGUI messageUI;
    bool inTrigger = false;
    int messageIndex = 0;
    PlayerController controller;

    private void Awake()
    {
        messageUI = GameObject.FindGameObjectWithTag("Message").GetComponent<TextMeshProUGUI>();
        controller = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(inTrigger && messageUI.text != messages[messageIndex])
        {
            messageUI.text = messages[messageIndex] + " [Space]";
            controller.canMove = false;
            controller.SetVelocity(Vector2.zero);
            Time.timeScale = 0;
        }

        if(inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            messageIndex++;
            if(messageIndex >= messages.Length)
            {
                Destroy(gameObject);
                messageUI.text = string.Empty;
                controller.canMove = true;
                Time.timeScale = 1;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

}

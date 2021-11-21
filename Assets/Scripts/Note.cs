using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour
{
    [SerializeField] string noteName;
    [SerializeField] string[] messages;

    int messageIndex = 0;
    TextMeshProUGUI prompt;
    TextMeshProUGUI messageUI;
    PlayerController controller;
    bool inTrigger = false;
    bool hasInteracted = false;
    bool read = false;

    private void Awake()
    {
        messageUI = GameObject.FindGameObjectWithTag("Message").GetComponent<TextMeshProUGUI>();
        prompt = GameObject.FindGameObjectWithTag("Prompt").GetComponent<TextMeshProUGUI>();
        controller = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.F)) hasInteracted = true;
        if (inTrigger && messageUI.text != messages[messageIndex] && hasInteracted)
        {
            messageUI.text = messages[messageIndex] + " [Space]";
            controller.canMove = false;
            controller.SetVelocity(Vector2.zero);
            Time.timeScale = 0;
            prompt.text = string.Empty;
        }

        if (inTrigger && Input.GetKeyDown(KeyCode.Space) && hasInteracted)
        {
            messageIndex++;
            if (messageIndex >= messages.Length)
            {
                hasInteracted = false;
                read = true;
                messageUI.text = string.Empty;
                controller.canMove = true;
                Time.timeScale = 1;
                messageIndex = 0;
                prompt.text = "Press F to read " + noteName + " (Read)";
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!read) prompt.text = "Press F to read " + noteName;
            else prompt.text = "Press F to read " + noteName + " (Read)";
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

    public bool GetRead()
    {
        return read;
    }
}

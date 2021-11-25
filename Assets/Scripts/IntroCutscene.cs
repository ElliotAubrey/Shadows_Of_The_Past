using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_min, text_full;
    [SerializeField] Image textboxBackground_min, textboxBackground_full;
    [SerializeField] Image image;
    [SerializeField] string[] words;
    [SerializeField] Sprite[] frames;
    [SerializeField] float[] timebetweenframes;
    [SerializeField] bool[] fullscreenText;

    int framecounter = 0;
    float time;
    bool over = false;
    bool notLoaded = true;

    private void Awake()
    {
        Time.timeScale = 1;
        image.sprite = frames[0];
        if (!fullscreenText[0]) text_min.text = words[0];
        else text_full.text = words[0];
        Cursor.visible = false;
        DecideFullscreen();
    }


    private void FixedUpdate()
    {
        Debug.Log(over);
        time += Time.deltaTime;
        if (framecounter == timebetweenframes.Length && notLoaded)
        {
            over = true;
            notLoaded = false;
            time = 0;
            SceneManager.UnloadSceneAsync("Intro");
            SceneManager.LoadScene("Main");
            Destroy(this);
        }
        if(!over)
        {
            if (time >= timebetweenframes[framecounter])
            {
                framecounter++;
                time = 0;
                DecideFullscreen();
                if (framecounter <= frames.Length)
                {
                    if (!fullscreenText[framecounter]) text_min.text = words[framecounter];
                    else text_full.text = words[framecounter];
                    image.sprite = frames[framecounter];
                }
            }
        }
    }

    private void DecideFullscreen()
    {
        if (fullscreenText[framecounter])
        {
            text_min.enabled = false;
            textboxBackground_min.enabled = false;
            text_full.enabled = true;
            textboxBackground_full.enabled = true;
            if(words[framecounter] == string.Empty) textboxBackground_full.enabled = false;
        }
        else
        {
            text_full.enabled = false;
            textboxBackground_full.enabled = false;
            text_min.enabled = true;
            textboxBackground_min.enabled = true;
            if (words[framecounter] == string.Empty) textboxBackground_min.enabled = false;
        }
    }
}

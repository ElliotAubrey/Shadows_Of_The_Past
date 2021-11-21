using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image image;
    [SerializeField] string[] words;
    [SerializeField] Sprite[] frames;
    [SerializeField] float timebetweenframes = 5f;

    float t;
    int framecounter = 0;
    float time;

    private void Awake()
    {
        t = timebetweenframes / 50;
        image.sprite = frames[0];
        text.text = words[0];
    }


    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time>= timebetweenframes)
        {
            framecounter++;
            time = 0;
            if (framecounter <= frames.Length)
            {
                text.text = words[framecounter];
                image.sprite = frames[framecounter];
            }

            else SceneManager.LoadScene("Main");
        }
    }
}

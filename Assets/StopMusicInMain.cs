using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicInMain : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<MusicBehaviour>().Stop();
    }
}

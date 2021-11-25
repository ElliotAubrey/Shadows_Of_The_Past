using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBehaviour : MonoBehaviour
{
    [SerializeField] FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField]
    [Range(0f, 1f)]
    public float musicOnOff;

    void Start()
    {
        DontDestroyOnLoad(this);
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
        musicOnOff = 0;
    }

    void FixedUpdate()
    {
        instance.setParameterByName("MusicOnOff", musicOnOff);
    }

    public void Stop()
    {
        musicOnOff = 1;
    }

    public void StartMusic()
    {
        musicOnOff = 0;
    }
}

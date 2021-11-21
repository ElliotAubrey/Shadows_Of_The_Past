using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class InsanitySound : MonoBehaviour
{
    [SerializeField] FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    [SerializeField]
    [Range(0f, 100f)]
    public float insanity;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    void FixedUpdate()
    {
        instance.setParameterByName("Insanity", insanity);
    }


    public void Stop()
    {
        Debug.Log("Stop");
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}

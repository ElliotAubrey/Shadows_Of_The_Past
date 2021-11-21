using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Hearing : MonoBehaviour, IPerk
{
    FMOD.Studio.VCA vcaAll;
    FMOD.Studio.VCA vcaHearing;
    bool perkEnabled = true;

    private void Awake()
    {
        vcaAll = FMODUnity.RuntimeManager.GetVCA("vca:/All");
        vcaHearing = FMODUnity.RuntimeManager.GetVCA("vca:/Hearing");
    }

    public void Disable()
    {
        vcaAll.setVolume(-55);
        vcaHearing.setVolume(0);
    }

    public void Enable()
    {
        perkEnabled = true;
    }

    public bool GetState()
    {
        return perkEnabled;
    }
}

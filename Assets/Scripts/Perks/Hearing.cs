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
        vcaAll.setVolume(.2f);
        vcaHearing.setVolume(.2f);
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

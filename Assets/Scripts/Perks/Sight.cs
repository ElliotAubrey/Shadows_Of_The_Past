using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Sight : MonoBehaviour, IPerk
{
    [SerializeField] Volume vol;
    [SerializeField] float enabledI, disabledI;

    bool perkEnabled = true;
    Vignette vig;

    private void Awake()
    {
        vol.profile.TryGet(out vig);
        if (!vig)
        {
            vol.profile.Add<Vignette>();
            vol.profile.TryGet(out vig);
            vig.intensity.value = 0.3f;
            Debug.Log("Added");
        }
    }
    public void Disable()
    {
        perkEnabled = false;
        vig.intensity.value = disabledI;
        Debug.Log("Vig disabled");
    }

    public void Enable()
    {
        perkEnabled = true;
        vig.intensity.value = enabledI;
    }

    public bool GetState()
    {
        return perkEnabled;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mind : MonoBehaviour, IPerk
{
    bool perkEnabled = true;
    PlayerSanity sanity;

    private void Awake()
    {
        sanity = FindObjectOfType<PlayerSanity>();
    }
    public void Disable()
    {
        perkEnabled = false;
        sanity.SetModifier(2);
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

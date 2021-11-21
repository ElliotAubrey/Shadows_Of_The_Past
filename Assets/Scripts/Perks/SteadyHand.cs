using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyHand : MonoBehaviour, IPerk
{
    bool perkEnabled = true;
    PlayerGun gun;

    private void Awake()
    {
        gun = FindObjectOfType<PlayerGun>();
    }
    public void Disable()
    {
        perkEnabled = false;
        gun.accuracy = 0.8f;
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

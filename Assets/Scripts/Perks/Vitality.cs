using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : MonoBehaviour, IPerk
{
    [SerializeField] HealthSystem playerHealth;
    bool perkEnabled = true;
    public void Disable()
    {
        perkEnabled = false;
        playerHealth.SetMaxHealth(50);
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

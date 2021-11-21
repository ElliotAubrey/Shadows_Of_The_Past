using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IPerk
{
    [SerializeField] PlayerController controller;
    bool perkEnabled = true;
    public void Disable()
    {
        perkEnabled = false;
        controller.MovementDisabled();
    }

    public void Enable()
    {
        perkEnabled = true;
        controller.MovementEnabled();
    }

    public bool GetState()
    {
        return perkEnabled;
    }
}

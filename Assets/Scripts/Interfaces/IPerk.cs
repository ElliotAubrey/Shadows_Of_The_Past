using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPerk
{
    public void Enable();
    public void Disable();
    public bool GetState();
}

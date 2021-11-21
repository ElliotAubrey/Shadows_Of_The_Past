using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
    [SerializeField] Button button;

    public void Disable()
    {
        button.transform.parent.gameObject.SetActive(false);
    }
}

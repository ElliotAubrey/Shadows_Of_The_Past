using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnPlay : MonoBehaviour
{
    [SerializeField] GameObject thing;

    private void Start()
    {
        thing.gameObject.SetActive(false);
    }
}

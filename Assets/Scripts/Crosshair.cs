using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}

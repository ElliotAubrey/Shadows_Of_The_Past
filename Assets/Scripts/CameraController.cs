using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        transform.position = new Vector3 (target.position.x, target.position.y, transform.transform.position.z);
    }
}

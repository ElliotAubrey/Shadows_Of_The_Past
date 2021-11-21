using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] AIPath aiPath;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceVelocity();
    }

    private void FaceVelocity()
    {
        direction = aiPath.desiredVelocity;
        transform.right = direction;
    }
}

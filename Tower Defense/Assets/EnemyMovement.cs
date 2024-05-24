using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    //The point we want to move to
    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        //On start make sure our target point is the first point in the array
        target = LevelManager.main.path[pathIndex];

    }

    private void Update()
    {

        //If we have reached the current target (within a margin), advance the index to the next target
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;



            //If we have reached the end of the path, destroy self
            if (pathIndex == LevelManager.main.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        
    }

}

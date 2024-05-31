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
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private int baseDamage = 2;

    //The point we want to move to
    private Transform target;
    public int pathIndex = 0;

    //private float baseSpeed;

    private void Start()
    {
        
        //On start make sure our target point is the first point in the array
        target = LevelManager.main.path[pathIndex];

    }

    private void Update()
    {

        //If we have reached the current target (within a margin), advance the index to the next target
        if (Vector2.Distance(target.position, transform.position) <= 0.05f)
        {
            transform.position = target.position;
            pathIndex++;



            //If we have reached the end of the path, destroy self
            if (pathIndex == LevelManager.main.path.Length)
            {
                Castle.main.TakeDamage(baseDamage);
                Debug.Log("Castle Health: " + Castle.main.GetHealth());
                EnemySpawner.onEnemyDestroy.Invoke();
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

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    public void SetPathIndex(int index)
    {
        pathIndex = index;
        target = LevelManager.main.path[pathIndex];
    }

    public void DelayResetSpeed()
    {
        StartCoroutine(SpeedReset());
    }

    private IEnumerator SpeedReset()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("Resetting Speed");
        moveSpeed = baseSpeed;

    }
}

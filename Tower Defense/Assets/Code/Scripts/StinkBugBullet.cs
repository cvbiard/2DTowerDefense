using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkBugBullet: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 0f;
    [SerializeField] private float bulletDamage = 1;

    private Vector2 direction;

    public void SetDirection(Vector2 _direction) 
    { 
        direction = _direction; 
    }

    public void MultiplyDamage(float multiplier)
    {
        bulletDamage = bulletDamage * multiplier;
    }

    private void FixedUpdate()
    {
       rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = 1;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;   
    }

    public void MultiplyDamage(float multiplier)
    {
        bulletDamage = bulletDamage * multiplier;
    }

    private void FixedUpdate()
    {
        if(!target)
        {
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Debug.Log("Dealing " + bulletDamage + " Damage");
        Destroy(gameObject);
    }

}

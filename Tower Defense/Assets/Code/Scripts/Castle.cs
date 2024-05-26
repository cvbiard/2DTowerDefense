using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public static Castle main;

    [Header("Attribute")]
    [SerializeField] private int health = 100;

    private void Awake()
    {
        main = this;
    }

    public void TakeDamage(int dmg)
    {
        health = health - dmg;
    }

    public int GetHealth()
    {
        return health;
    }
}

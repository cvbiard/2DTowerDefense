using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Castle : MonoBehaviour
{
    public static Castle main;

    [Header("References")]
    [SerializeField] TextMeshProUGUI healthUI;

    [Header("Attribute")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.text = "Health: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth = currentHealth - dmg;
        healthUI.text = "Health: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}

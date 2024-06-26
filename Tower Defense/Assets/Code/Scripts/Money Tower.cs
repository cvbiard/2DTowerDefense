using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator anim;

    [Header("Attribute")]
    [SerializeField] private int basePayout = 200;
    [SerializeField] private int roundMultiplier = 10;

    void OnEnable()
    {
        EnemySpawner.main.OnRoundEnded += WhenRoundEnds;
    }

    void OnDisable()
    {
        EnemySpawner.main.OnRoundEnded -= WhenRoundEnds;
    }

    void WhenRoundEnds(int round)
    {
        GiveMoney(basePayout +(round * roundMultiplier));
    }
    private void GiveMoney(int amount)
    {
        anim.enabled = true;
        anim.Play("dollarAnim", -1, 0f);
        LevelManager.main.IncreaseCurrency(amount);
    }
}

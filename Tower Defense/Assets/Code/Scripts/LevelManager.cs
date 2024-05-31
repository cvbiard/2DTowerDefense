using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;


    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private void Awake()
    {
        main = this;
    }

    public void Start()
    {
        currency = 50000;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency)
        { 
            //Buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to purchase this");
            return false;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public bool[] foodFor;
    [SerializeField] public bool[] waterFor;
    [SerializeField] public bool[] coverFor;

    [SerializeField] public bool[] currentFoodFor;
    [SerializeField] public bool[] currentWaterFor;
    [SerializeField] public bool[] currentCoverFor;

    //[SerializeField] private FlowerCore flowerCore;

    private bool canBoost = false;

    public bool GetCanBoost()
    {
        return canBoost;
    }

    public void SetCanBoost(bool _canBoost)
    {
        canBoost = _canBoost;
    }


    public void DisableBoost()
    {
        for(int i = 0; i< foodFor.Length; i++)
        {
            currentFoodFor[i] = false;
            currentWaterFor[i] = false;
            currentCoverFor[i] = false;
        }
    }

    public void EnableBoost()
    {
        for (int i = 0; i < foodFor.Length; i++)
        {
            currentFoodFor[i] = foodFor[i];
            currentWaterFor[i] = waterFor[i];
            currentCoverFor[i] = coverFor[i];
        }
    }
}

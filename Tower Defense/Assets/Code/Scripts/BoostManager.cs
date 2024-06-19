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

    [SerializeField] private FlowerCore flowerCore;

}

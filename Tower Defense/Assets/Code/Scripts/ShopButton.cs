using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI buttonText;

    [Header("Attributes")]
    [SerializeField] int towerIndex;

    private void Start()
    {
        buttonText.text = BuildManager.main.GetTower(towerIndex).name + " ($" + BuildManager.main.GetTower(towerIndex).cost.ToString() + ")";
    }
}

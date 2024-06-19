using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite[] growthSprites;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoostManager boostManager;


    [Header("Attribute")]
    [SerializeField] private int maxWaterLevel = 0;
    [SerializeField] private int reqWaterToGrow = 0;
    [SerializeField] private int growthLevelBoost = 0;
    [SerializeField] private int waterLevelBoost = 0;
    //[SerializeField] private float placingRange = 3f;
    //[SerializeField] private int sellValue = 100;
    //[SerializeField] private int turnsToGrow = 3;
    //[SerializeField] private float targetingRange = 3f;

    private int turnsAlive = 0;
    private int currentWaterLevel = 0;
    private int currentGrowthLevel = 0;

    private void Update()
    {
        if (growthLevelBoost <= currentGrowthLevel)
        {
            boostManager.SetCanBoost(true);
        }
        else
        {
            boostManager.SetCanBoost(false);
        }
    }

    void OnEnable()
    {
        EnemySpawner.main.OnRoundEnded += Grow;
    }

    void OnDisable()
    {
        EnemySpawner.main.OnRoundEnded -= Grow;
    }

    void Grow(int round)
    {
        turnsAlive++;
        updateGrowth();
    }

    private void updateGrowth()
    {
        if(currentWaterLevel >= reqWaterToGrow)
        {
            if(currentGrowthLevel < growthSprites.Length)
            {
                currentGrowthLevel++;
                sr.sprite = growthSprites[currentGrowthLevel];
                currentWaterLevel--;
            }
            
            return;
        }
        Debug.Log("Not watered enough to grow");
        
        
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(ToolManager.main.GetCurrentTool() == 4)
            {
                if(LevelManager.main.currency >= ToolManager.main.GetWateringCost())
                {
                    if(maxWaterLevel > currentWaterLevel)
                    {
                        Debug.Log("Watering");
                        LevelManager.main.SpendCurrency(ToolManager.main.GetWateringCost());
                        currentWaterLevel++;
                        return;
                    }
                    Debug.Log("Fully Watered");
                    return;
                }
                Debug.Log("Can't afford to water");
                return;
            }
        }
    }
}

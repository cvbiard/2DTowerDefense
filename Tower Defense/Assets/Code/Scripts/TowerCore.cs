using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCore : MonoBehaviour
{
    //This component is the core of every tower, containing elements every tower has
    [Header("References")]
    [SerializeField] private SpriteRenderer srenderer;
    [SerializeField] private GameObject towerRangeVisual;
    [SerializeField] private BoostManager boostManager;
    [SerializeField] private LayerMask boostMask;


    [Header("Attribute")]
    [SerializeField] private int towerID = 0;
    [SerializeField] private float placingRange = 3f;
    [SerializeField] private int sellValue = 100;
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private int requiredGroundID = 0;

    private int foodNear = 0;
    private int waterNear = 0;
    private int coverNear = 0;

    private int circleOverlapFrame = 10;
    private int circleOverlapCurrentFrame = 0;

    private void Start()
    {
        if(towerRangeVisual != null)
        {
            towerRangeVisual.transform.localScale = new Vector2(targetingRange * 2, targetingRange * 2);
            towerRangeVisual.SetActive(false);
        }
        
    }
    private void Update()
    {
        if(circleOverlapCurrentFrame == circleOverlapFrame)
        {
            int tempFood = 0;
            int tempWater = 0;
            int tempCover = 0;
            circleOverlapCurrentFrame = 0;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, boostMask);

            for(int i = 0; i<hits.Length; i++)
            {
                if (hits[i].gameObject.GetComponent<BoostManager>().foodFor[towerID] == true)
                {
                    tempFood++;
                }
                if (hits[i].gameObject.GetComponent<BoostManager>().waterFor[towerID] == true)
                {
                    tempWater++;
                }
                if (hits[i].gameObject.GetComponent<BoostManager>().coverFor[towerID] == true)
                {
                    tempCover++;
                }
            }
            foodNear = tempFood;
            waterNear = tempWater;
            coverNear = tempCover;
        }
        else
        {
            circleOverlapCurrentFrame++;
        }
    }
    public Sprite GetSprite()
    { 
        return srenderer.sprite;
    }

    public float GetPlacingRange()
    {
        return placingRange;
    }
    public float GetTargetingRange()
    {
        return targetingRange;
    }


    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            
                LevelManager.main.IncreaseCurrency(sellValue);
                Destroy(gameObject);
            
            
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log("Showing targeting range");
        if (towerRangeVisual != null)
        {
            
            towerRangeVisual.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (towerRangeVisual != null)
        {
            towerRangeVisual.SetActive(false);
        }
    }

    public int GetRequiredGround()
    {
        return requiredGroundID;
    }

    public int GetFoodNear()
    {
        return foodNear;
    }
    public int GetWaterNear()
    {
        return waterNear;
    }
    public int GetCoverNear()
    {
        return coverNear;
    }

    public void IncFoodNear()
    {
        Debug.Log("Incrementing food near");
        foodNear++;
    }
    public void IncWaterNear()
    {
        waterNear++;
    }
    public void IncCoverNear()
    {
        coverNear++;
    }
    public void DecFoodNear()
    {
        Debug.Log("Dec food near");
        foodNear = foodNear -1;
    }
    public void DecWaterNear()
    {
        waterNear--;
    }
    public void DecCoverNear()
    {
        coverNear--;
    }

    public int GetTowerID()
    {
        return towerID;
    }

    public int GetSellValue()
    {
        return sellValue;
    }
}

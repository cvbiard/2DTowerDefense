using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class TowerCore : MonoBehaviour
{
    //This component is the core of every tower, containing elements every tower has
    [Header("References")]
    [SerializeField] private SpriteRenderer srenderer;
    [SerializeField] private GameObject towerRangeVisual;


    [Header("Attribute")]
    [SerializeField] private float placingRange = 3f;
    [SerializeField] private int sellValue = 100;
    [SerializeField] private float targetingRange = 3f;


    private void Start()
    {
        if(towerRangeVisual != null)
        {
            towerRangeVisual.transform.localScale = new Vector2(targetingRange * 2, targetingRange * 2);
            towerRangeVisual.SetActive(false);
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
            //sell
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

}

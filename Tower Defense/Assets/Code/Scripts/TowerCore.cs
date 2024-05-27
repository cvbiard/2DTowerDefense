using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCore : MonoBehaviour
{
    //This component is the core of every tower, containing elements every tower has
    [Header("References")]
    [SerializeField] private SpriteRenderer srenderer;
    

    [Header("Attribute")]
    [SerializeField] private float placingRange = 3f;

    public Sprite GetSprite()
    { 
        return srenderer.sprite;
    }

    public float GetPlacingRange()
    {
        return placingRange;
    }


}

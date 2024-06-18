using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite[] groundTypeSprites;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private LayerMask turretMask;

    [Header("Attribute")]
    [SerializeField] private int changeGroundCost = 10;




    private int currentGroundType = 0;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1f, (Vector2)transform.position, 0f, turretMask);
            
            if(hits.Length < 1 && ToolManager.main.GetCurrentTool() != 4)
            {
                if (changeGroundCost > LevelManager.main.currency)
                {
                    Debug.Log("You can't afford this tower");
                    return;
                }

                if(currentGroundType != ToolManager.main.GetCurrentTool())
                {
                    Debug.Log("changing ground");
                    //groundTypes[ToolManager.main.GetCurrentTool()].SetActive(true);
                    sr.sprite = groundTypeSprites[ToolManager.main.GetCurrentTool()];
                    currentGroundType = ToolManager.main.GetCurrentTool();
                    LevelManager.main.SpendCurrency(changeGroundCost);
                }
               

            }
            
        }
    }

    public int GetCurrentGround()
    {
        return currentGroundType;
    }

}

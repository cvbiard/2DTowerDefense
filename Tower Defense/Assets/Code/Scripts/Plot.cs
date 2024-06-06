using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] groundTypes;
    [SerializeField] private LayerMask turretMask;

    [Header("Attribute")]
    [SerializeField] private int changeGroundCost = 10;




    private int currentGroundType = 0;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1f, (Vector2)transform.position, 0f, turretMask);
            
            if(hits.Length < 1)
            {
                if (changeGroundCost > LevelManager.main.currency)
                {
                    Debug.Log("You can't afford this tower");
                    return;
                }

                    for (int i = 0; i < groundTypes.Length; i++)
                {
                    groundTypes[i].SetActive(false);
                }

                Debug.Log("changing ground");
                groundTypes[ToolManager.main.GetCurrentTool()].SetActive(true);
                currentGroundType = ToolManager.main.GetCurrentTool();
                LevelManager.main.SpendCurrency(changeGroundCost);

            }
            
        }
    }

    public int GetCurrentGround()
    {
        return currentGroundType;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager main;

    private int currentToolID = 0;
    
    [Header("Attribute")]
    [SerializeField] private int wateringCost = 10;

    private void Awake()
    {
        main = this;
    }
    
    public int GetCurrentTool()
    { 
        return currentToolID;
    }
    public int GetWateringCost()
    {
        return wateringCost;
    }
    public void SetCurrentTool(int toolID)
    {
        currentToolID = toolID;
    }
}

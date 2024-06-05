using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager main;

    private int currentToolID = 0;

    private void Awake()
    {
        main = this;
    }
    
    public int GetCurrentTool()
    { 
        return currentToolID;
    }
    public void SetCurrentTool(int toolID)
    {
        currentToolID = toolID;
    }
}

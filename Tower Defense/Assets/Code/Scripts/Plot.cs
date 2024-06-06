using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] groundTypes;

    private int currentGroundType = 0;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < groundTypes.Length; i++) 
            {
                groundTypes[i].SetActive(false);
            }

            Debug.Log("changing ground");
            groundTypes[ToolManager.main.GetCurrentTool()].SetActive(true);
            currentGroundType = ToolManager.main.GetCurrentTool();
        }
    }

    public int GetCurrentGround()
    {
        return currentGroundType;
    }

}
